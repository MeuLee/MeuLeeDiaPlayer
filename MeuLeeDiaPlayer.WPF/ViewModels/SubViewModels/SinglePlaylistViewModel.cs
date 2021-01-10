using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using MeuLeeDiaPlayer.WPF.Commands.SinglePlaylist;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.ViewModels.SubViewModels
{
    public class SinglePlaylistViewModel : BaseViewModel
    {
        public ICommand UpdateCurrentSongCommand { get; }
        public ICommand PlayPlaylistCommand { get; }
        public ICommand ShowEditPlaylistDialogCommand { get; }
        public ICommand DeletePlaylistCommand { get; }
        public ISoundPlayerManager SoundPlayerManager { get; }
        public IPlaylistHolder PlaylistHolder { get; }

        public SinglePlaylistViewModel(
            UpdateCurrentSongCommand updateCurrentSongCommand,
            PlayPlaylistCommand playPlaylistCommand,
            ShowEditPlaylistDialogCommand showEditPlaylistDialogCommand,
            DeletePlaylistCommand deletePlaylistCommand,
            ISoundPlayerManager soundPlayerManager,
            IPlaylistHolder playlistHolder)
        {
            UpdateCurrentSongCommand = updateCurrentSongCommand;
            PlayPlaylistCommand = playPlaylistCommand;
            ShowEditPlaylistDialogCommand = showEditPlaylistDialogCommand;
            DeletePlaylistCommand = deletePlaylistCommand;
            SoundPlayerManager = soundPlayerManager;
            PlaylistHolder = playlistHolder;
        }
    }
}
