using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace MeuLeeDiaPlayer.YoutubeExplodeWrapper
{
    // to be used as a singleton with DI framework
    public class YoutubeVideoDownloader : IYoutubeVideoDownloader
    {
        private readonly StreamClient _streamClient;
        private readonly PlaylistClient _playlistClient;
        private readonly VideoClient _videoClient;

        public YoutubeVideoDownloader()
        {
            var ytClient = new YoutubeClient();
            _streamClient = ytClient.Videos.Streams;
            _playlistClient = ytClient.Playlists;
            _videoClient = ytClient.Videos;
        }

        public async Task<string> TryDownloadVideo(string folder, string videoIdOrUrl, IProgress<double> progress = null, CancellationToken token = default)
        {
            try
            {
                var videoId = new VideoId(videoIdOrUrl);

                var videoInfo = await _videoClient.GetAsync(videoId);
                var streamManifest = await _streamClient.GetManifestAsync(videoId).ConfigureAwait(false);
                var streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();

                string filePath = $"{folder}\\{videoInfo.Title}.{streamInfo.Container.Name}";
                await _streamClient.DownloadAsync(streamInfo, filePath, progress, token).ConfigureAwait(false);
                return filePath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<string>> TryDownloadPlaylist(string folder, string playlistIdOrUrl, IProgress<double> progress = null, CancellationToken token = default)
        {
            try
            {
                var playlistId = new PlaylistId(playlistIdOrUrl);
                var videos = (await _playlistClient.GetVideosAsync(playlistId)).ToList();

                var multiProgress = new MultiProgress(progress, videos.Count);
                var paths = new List<string>();
                foreach (var video in videos)
                {
                    multiProgress.Add(video.Id.Value);
                    var currentProgress = new Progress<double>(p => multiProgress.Update(video.Id.Value, p));
                    string path = await TryDownloadVideo(folder, video.Id, currentProgress, token);
                    paths.Add(path);
                }
                return paths;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
