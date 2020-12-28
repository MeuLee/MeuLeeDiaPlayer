using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.SoundPlayer;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class UpdateCurrentSongCommand : BaseCommand
    {
        private readonly ISoundPlayerManager _soundPlayerManager;

        public UpdateCurrentSongCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public override void Execute(object parameter)
        {
            if (parameter is not SongDto song) return;

            if (_soundPlayerManager.CurrentSong == song)
            {
                _soundPlayerManager.PauseOrResume();
            }
            else
            {
                _soundPlayerManager.PlaySong(song);
            }
        }
    }
}
