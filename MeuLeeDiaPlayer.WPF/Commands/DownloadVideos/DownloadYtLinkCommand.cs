using MeuLeeDiaPlayer.EntityFramework;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.Services.UrlValidator;
using MeuLeeDiaPlayer.YoutubeExplodeWrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.WPF.Commands.DownloadVideos
{
    public class DownloadYtLinkCommand : BaseCommand
    {
        private readonly IYoutubeUrlValidator _youtubeUrlValidator;
        private readonly IYoutubeVideoDownloader _youtubeVideoDownloader;
        private readonly ISongLoader _songLoader;

        public DownloadYtLinkCommand(IYoutubeUrlValidator youtubeUrlValidator, IYoutubeVideoDownloader youtubeVideoDownloader, ISongLoader songLoader)
        {
            _youtubeUrlValidator = youtubeUrlValidator;
            _youtubeVideoDownloader = youtubeVideoDownloader;
            _songLoader = songLoader;
        }

        public override async void Execute(object parameter)
        {
            if (parameter is not string input) return;
            var ytUrlType = _youtubeUrlValidator.GetYoutubeUrlType(input);
            var paths = await GetDownloadedVideosPath(input); // try catch exception below
            foreach (var path in paths)
            {
                string songTitle = path[(path.LastIndexOf('\\') + 1)..path.LastIndexOf('.')];
                var song = new Song { Artist = "Unkown Artist", Path = path, Title = songTitle, PlaylistSongs = new() };
                await _songLoader.AddSongAsync(song);
            }
        }

        private async Task<IEnumerable<string>> GetDownloadedVideosPath(string youtubeUrl)
        {
#pragma warning disable CS8524 // every enum value is covered, and enums aren't nullable.
            return _youtubeUrlValidator.GetYoutubeUrlType(youtubeUrl) switch
#pragma warning restore CS8524
            {
                YoutubeUrlType.Playlist => await _youtubeVideoDownloader.TryDownloadPlaylistAsync(Constants.DefaultSongsLocation, youtubeUrl),
                YoutubeUrlType.Video => new string[] { await _youtubeVideoDownloader.TryDownloadVideoAsync(Constants.DefaultSongsLocation, youtubeUrl) },
                YoutubeUrlType.Neither => throw new ArgumentException("Provided input is not a playlist nor a video", nameof(youtubeUrl))
            };
        }
    }
}
