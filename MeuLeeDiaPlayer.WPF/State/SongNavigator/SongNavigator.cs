using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.SoundPlayer;
using MeuLeeDiaPlayer.WPF.Commands;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.State.SongNavigator
{
    public class SongNavigator : ObservableObject, ISongNavigator
    {
        public ICommand UpdateCurrentSongCommand { get; }
        public ISoundPlayerManager SoundPlayerManager { get; }

        public PlaylistDto CurrentPlaylist
        {
            get => _currentPlaylist;
            set
            {
                _currentPlaylist = value;
                OnPropertyChanged(nameof(CurrentPlaylist));
            }
        }

        private PlaylistDto _currentPlaylist;

        public SongNavigator(ISoundPlayerManager soundPlayerManager)
        {
            UpdateCurrentSongCommand = new UpdateCurrentSongCommand(soundPlayerManager);
            SoundPlayerManager = soundPlayerManager;
        }
    }
}
