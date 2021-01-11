﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.YoutubeExplodeWrapper
{
    public interface IYoutubeVideoDownloader
    {
        Task<string> TryDownloadVideo(string filePath, string videoIdOrUrl, IProgress<double> progress = null, CancellationToken token = default);
        Task<IEnumerable<string>> TryDownloadPlaylist(string folder, string playlistIdOrUrl, IProgress<double> progress = null, CancellationToken token = default);
    }
}