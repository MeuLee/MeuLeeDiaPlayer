using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.SoundPlayer;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.SongNavigator
{
    public interface ISongNavigator
    {
        PlaylistDto CurrentPlaylist { get; set; }

        ICommand UpdateCurrentSongCommand { get; }
        ISoundPlayerManager SoundPlayerManager { get; }
    }
}
