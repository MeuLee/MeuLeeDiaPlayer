using System;
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
    public class YoutubeVideoDownloader : IVideoDownloader
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

        public async Task DownloadVideo(string folder, string videoIdOrUrl, IProgress<double> progress, CancellationToken token = default)
        {
            var videoId = new VideoId(videoIdOrUrl);

            var videoInfo = await _videoClient.GetAsync(videoId);
            var streamManifest = await _streamClient.GetManifestAsync(videoId).ConfigureAwait(false);
            var streamInfo = streamManifest.GetAudioOnly().WithHighestBitrate();

            string filePath = $"{folder}\\{videoInfo.Title}.{streamInfo.Container.Name}";
            await _streamClient.DownloadAsync(streamInfo, filePath, progress, token).ConfigureAwait(false);
        }

        // should use parallel here if the method is slow
        public async Task DownloadPlaylist(string folder, string playlistIdOrUrl, IProgress<double> progress, CancellationToken token = default)
        {
            var playlistId = new PlaylistId(playlistIdOrUrl);
            var videos = (await _playlistClient.GetVideosAsync(playlistId)).ToList();

            var multiProgress = new MultiProgress(progress, videos.Count);
            foreach (var video in videos)
            {
                multiProgress.Add(video.Id.Value);
                var currentProgress = new Progress<double>(p => multiProgress.Update(video.Id.Value, p));
                await DownloadVideo(folder, video.Id, currentProgress, token);
            }
        }
    }
}
