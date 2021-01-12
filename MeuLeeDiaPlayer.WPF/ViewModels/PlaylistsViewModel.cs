using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.PlaylistRetrievers;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Commands.Playlists;
using MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class PlaylistsViewModel : BaseViewModel
    {
        public IPlaylistHolder PlaylistHolder { get; }
        public ICommand UpdateCurrentPlaylistCommand { get; }
        public ICommand ShowCreatePlaylistDialogCommand { get; }
        public SinglePlaylistViewModel SinglePlaylistViewModel { get; }

        private readonly ISongLoader _songLoader;
        private readonly IPlaylistRetriever _playlistRetriever;

        public PlaylistsViewModel(
            ISongLoader songLoader,
            IPlaylistRetriever playlistRetriever,
            IPlaylistHolder playlistHolder,
            UpdateCurrentPlaylistCommand updateCurrentPlaylistCommand,
            ShowCreatePlaylistDialogCommand showCreatePlaylistDialogCommand,
            SinglePlaylistViewModel singlePlaylistVm)
        {
            _songLoader = songLoader;
            _playlistRetriever = playlistRetriever;
            PlaylistHolder = playlistHolder;
            UpdateCurrentPlaylistCommand = updateCurrentPlaylistCommand;
            ShowCreatePlaylistDialogCommand = showCreatePlaylistDialogCommand;
            SinglePlaylistViewModel = singlePlaylistVm;

            LoadPlaylists()
                .ContinueWith(task =>
                {
                    MessageBox.Show(task.Exception.Message);
                }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task LoadPlaylists()
        {
            await _playlistRetriever.LoadPlaylists();
            await _songLoader.LoadSongsAsync(PlaylistHolder.Playlists);
        }
    }
}
