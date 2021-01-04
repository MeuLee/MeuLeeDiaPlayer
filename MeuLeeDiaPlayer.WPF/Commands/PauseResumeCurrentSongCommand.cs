using MeuLeeDiaPlayer.Services.SoundPlayer;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class PauseResumeCurrentSongCommand : BaseCommand
    {
        private readonly ISoundPlayerManager _soundPlayerManager;

        public PauseResumeCurrentSongCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public override void Execute(object parameter)
        {
            _soundPlayerManager.PauseOrResume();
        }
    }
}
