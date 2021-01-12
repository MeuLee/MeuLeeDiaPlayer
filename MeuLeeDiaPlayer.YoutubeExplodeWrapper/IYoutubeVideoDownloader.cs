using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.YoutubeExplodeWrapper
{
    public interface IYoutubeVideoDownloader
    {
        Task<string> TryDownloadVideoAsync(string filePath, string videoIdOrUrl, IProgress<double> progress = null, CancellationToken token = default);
        Task<IEnumerable<string>> TryDownloadPlaylistAsync(string folder, string playlistIdOrUrl, IProgress<double> progress = null, CancellationToken token = default);
    }
}