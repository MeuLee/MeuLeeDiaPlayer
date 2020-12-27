using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.WPF.Commands;
using MeuLeeDiaPlayer.WPF.ViewModels;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.PlaylistNavigator
{
    public class PlaylistNavigator : ObservableObject, IPlaylistNavigator
    {
        public ICommand UpdateCurrentPlaylistCommand { get; }

        public PlaylistNavigator(PlaylistsViewModel playlistViewModel)
        {
            UpdateCurrentPlaylistCommand = new UpdateCurrentPlaylistCommand(playlistViewModel);
        }
    }
}
