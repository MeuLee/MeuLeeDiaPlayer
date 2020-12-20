using System;
using System.Threading;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.YoutubeExplodeWrapper
{
    public interface IVideoDownloader
    {
        Task DownloadVideo(string filePath, string videoIdOrUrl, IProgress<double> progress, CancellationToken token = default);
        Task DownloadPlaylist(string folder, string playlistIdOrUrl, IProgress<double> progress, CancellationToken token = default);
    }
}