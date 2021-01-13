using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SongLoaders;

namespace MeuLeeDiaPlayer.WPF.Commands.DownloadVideos
{
    public class DeleteSongCommand : BaseCommand
    {
        private readonly ISongLoader _songLoader;
        private readonly IPlaylistHolder _playlistHolder;

        public DeleteSongCommand(ISongLoader songLoader, IPlaylistHolder playlistHolder)
        {
            _songLoader = songLoader;
            _playlistHolder = playlistHolder;
        }

        public override async void Execute(object parameter)
        {
            if (parameter is not SongDto song) return;
            await _songLoader.DeleteSongAsync(song.Id);
            _playlistHolder.RemoveSong(song.Id);
        }
    }
}
