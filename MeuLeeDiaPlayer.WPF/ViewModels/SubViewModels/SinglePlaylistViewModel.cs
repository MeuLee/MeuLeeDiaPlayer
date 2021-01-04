using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using MeuLeeDiaPlayer.WPF.Commands;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels
{
    public class SinglePlaylistViewModel : BaseViewModel
    {
        public ICommand UpdateCurrentSongCommand { get; }
        public ICommand PlayPlaylistCommand { get; }
        public ISoundPlayerManager SoundPlayerManager { get; }
        public IPlaylistHolder PlaylistHolder { get; }

        public SinglePlaylistViewModel(
            UpdateCurrentSongCommand updateCurrentSongCommand,
            PlayPlaylistCommand playPlaylistCommand,
            ISoundPlayerManager soundPlayerManager,
            IPlaylistHolder playlistHolder)
        {
            UpdateCurrentSongCommand = updateCurrentSongCommand;
            PlayPlaylistCommand = playPlaylistCommand;
            SoundPlayerManager = soundPlayerManager;
            PlaylistHolder = playlistHolder;
        }
    }
}
