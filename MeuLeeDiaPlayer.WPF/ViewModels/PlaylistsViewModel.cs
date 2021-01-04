using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.PlaylistRetrievers;
using MeuLeeDiaPlayer.Services.SongLoaders;
using MeuLeeDiaPlayer.WPF.Commands;
using MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels;
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

            LoadPlaylists()
                .ContinueWith(task =>
                {
                    // handle exceptions
                }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task LoadPlaylists()
        {
            await _playlistRetriever.LoadPlaylists();
            await Task.Run(() => _songLoader.LoadSongs(PlaylistHolder.Playlists)
                .ContinueWith(task =>
                {
                    // handle exceptions
                }, TaskContinuationOptions.OnlyOnFaulted));
        }
    }
}
