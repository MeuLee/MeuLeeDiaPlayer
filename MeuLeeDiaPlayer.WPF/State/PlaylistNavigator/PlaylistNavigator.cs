using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.WPF.Commands;
using MeuLeeDiaPlayer.WPF.Models;
using MeuLeeDiaPlayer.WPF.ViewModels;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.PlaylistNavigator
{
    public class PlaylistNavigator : ObservableObject, IPlaylistNavigator
    {
        private Playlist _currentPlaylist;

        public Playlist CurrentPlaylist
        {
            get => _currentPlaylist;
            set
            {
                _currentPlaylist = value;
                OnPropertyChanged(nameof(CurrentPlaylist));
            }
        }

        public ICommand UpdateCurrentPlaylistCommand { get; }

        public PlaylistNavigator(PlaylistsViewModel playlistViewModel)
        {
            UpdateCurrentPlaylistCommand = new UpdateCurrentPlaylistCommand(playlistViewModel);
        }
    }
}
