using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using MeuLeeDiaPlayer.WPF.Commands;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.ViewModels
{
    public class SinglePlaylistViewModel : BaseViewModel
    {
        public ICommand UpdateCurrentSongCommand { get; }
        public ISoundPlayerManager SoundPlayerManager { get; }
        public IPlaylistHolder PlaylistHolder { get; }

        public SinglePlaylistViewModel(
            UpdateCurrentSongCommand updateCurrentSongCommand,
            ISoundPlayerManager soundPlayerManager,
            IPlaylistHolder playlistHolder)
        {
            UpdateCurrentSongCommand = updateCurrentSongCommand;
            SoundPlayerManager = soundPlayerManager;
            PlaylistHolder = playlistHolder;
        }
    }
}
