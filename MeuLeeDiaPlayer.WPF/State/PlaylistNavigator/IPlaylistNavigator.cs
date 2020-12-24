using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.PlaylistNavigator
{
    public interface IPlaylistNavigator
    {
        public Playlist CurrentPlaylist { get; set; }
        public ICommand UpdateCurrentPlaylistCommand { get; }
    }
}
