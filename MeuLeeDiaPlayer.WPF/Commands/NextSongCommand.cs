using MeuLeeDiaPlayer.Services.SoundPlayer;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class NextSongCommand : BaseCommand
    {
        private ISoundPlayerManager _soundPlayerManager;

        public NextSongCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public override void Execute(object parameter)
        {
            _soundPlayerManager.PlayNext();
        }
    }
}
