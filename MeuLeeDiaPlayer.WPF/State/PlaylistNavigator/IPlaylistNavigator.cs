using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.PlaylistNavigator
{
    public interface IPlaylistNavigator
    {
        public ICommand UpdateCurrentPlaylistCommand { get; }
    }
}
