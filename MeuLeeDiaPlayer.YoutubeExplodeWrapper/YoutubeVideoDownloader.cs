using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace MeuLeeDiaPlayer.YoutubeExplodeWrapper
{
    // to be used as a singleton with DI framework
    public class YoutubeVideoDownloader : IYoutubeVideoDownloader
    {
        private readonly PlaylistClient _playlistClient;
        private readonly VideoClient _videoClient;
        private const string _fileExtension = "mp3";

        public YoutubeVideoDownloader()
        {
            var ytClient = new YoutubeClient();
            _playlistClient = ytClient.Playlists;
            _videoClient = ytClient.Videos;
        }

        public async Task<IEnumerable<string>> TryDownloadPlaylistAsync(string folder, string playlistIdOrUrl, IProgress<double> progress = null, CancellationToken token = default)
        {
            try
            {
                var playlistId = new PlaylistId(playlistIdOrUrl);
                var videos = (await _playlistClient.GetVideosAsync(playlistId)).ToList();

                var multiProgress = new MultiProgress(progress, videos.Count);

                var paths = await Task.WhenAll(videos.Select(video =>
                {
                    multiProgress.Add(video.Id.Value);
                    var currentProgress = new Progress<double>(p => multiProgress.Update(video.Id.Value, p));
                    return TryDownloadVideoAsync(folder, video.Id, currentProgress, token);
                }));

                return paths.Where(path => !string.IsNullOrWhiteSpace(path));
            }
            catch (Exception ex)
            {
                return Array.Empty<string>();
            }
        }

        public async Task<string> TryDownloadVideoAsync(string folder, string videoIdOrUrl, IProgress<double> progress = null, CancellationToken token = default)
        {
            try
            {
                var videoId = new VideoId(videoIdOrUrl);
                var bestStreamInfo = await GetBestStreamInfoAsync(videoId);
                if (bestStreamInfo == null) return null;
                string path = await GetCompletePath(folder, videoId);
                return await DownloadVideoAsync(path, bestStreamInfo, progress, token);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<IAudioStreamInfo> GetBestStreamInfoAsync(VideoId videoId)
        {
            var manifest = await _videoClient.Streams.GetManifestAsync(videoId);
            return manifest
                .GetAudio()
                .OrderByDescending(s => s.Container == Container.WebM)
                .ThenByDescending(s => s.Bitrate)
                .FirstOrDefault();
        }

        private async Task<string> GetCompletePath(string folder, VideoId videoId)
        {
            var videoInfo = await _videoClient.GetAsync(videoId);
            return $@"{folder}\{videoInfo.Title}.{_fileExtension}";
        }

        private async Task<string> DownloadVideoAsync(string path, IAudioStreamInfo audioStreamInfo, IProgress<double> progress = null, CancellationToken token = default)
        {
            var conversion = new ConversionRequestBuilder(path)
                .SetFormat(_fileExtension)
                .SetPreset(ConversionPreset.Medium)
                .Build();
            await _videoClient.DownloadAsync(new List<IStreamInfo> { audioStreamInfo }, conversion, progress, token);
            return path;
        }
    }
}
