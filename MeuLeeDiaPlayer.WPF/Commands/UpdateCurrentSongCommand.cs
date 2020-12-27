using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.SoundPlayer;
using System;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class UpdateCurrentSongCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ISoundPlayerManager _soundPlayerManager;

        public UpdateCurrentSongCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public bool CanExecute(object parameter)
        {
            return true; // song.FileReader != null, will have to use CanExecuteChanged 
        }

        public void Execute(object parameter)
        {
            if (parameter is not SongDto song) return;
            if (_soundPlayerManager.CurrentSong == song)
            {
                _soundPlayerManager.PauseOrResume();
            }
            else
            {
                _soundPlayerManager.Play(song);
            }
        }
    }
}
