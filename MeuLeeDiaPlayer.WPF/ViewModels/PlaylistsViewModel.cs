using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.PlaylistRetrievers;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class PlaylistsViewModel : BaseViewModel
    {
        public IPlaylistHolder PlaylistHolder { get; }
        public ICommand UpdateCurrentPlaylistCommand { get; }
        public SinglePlaylistViewModel SinglePlaylistViewModel { get; }

        private readonly ISongLoader _songLoader;
        private readonly IPlaylistRetriever _playlistRetriever;

        public PlaylistsViewModel(
            ISongLoader songLoader,
            IPlaylistRetriever playlistRetriever,
            IPlaylistHolder playlistHolder,
            UpdateCurrentPlaylistCommand command,
            SinglePlaylistViewModel singlePlaylistVm)
        {
            _songLoader = songLoader;
            _playlistRetriever = playlistRetriever;
            PlaylistHolder = playlistHolder;
            UpdateCurrentPlaylistCommand = command;
            SinglePlaylistViewModel = singlePlaylistVm;

            LoadPlaylists();
        }

        private void LoadPlaylists()
        {
            var fetchPlaylistsTask = _playlistRetriever.LoadPlaylists();
            var loadSongsTask = _songLoader.LoadSongs();
            Task.WhenAll(fetchPlaylistsTask, loadSongsTask).ContinueWith(task => InitializePlaylists());
        }

        private void InitializePlaylists()
        {
            foreach (var playlist in PlaylistHolder.Playlists)
            {
                foreach (var song in playlist.Songs)
                {
                    song.FileReader = _songLoader.Songs[song.Path];
                }
            }
        }
    }
}
