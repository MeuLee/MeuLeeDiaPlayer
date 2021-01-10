using MeuLeeDiaPlayer.Services.SoundPlayer;

namespace MeuLeeDiaPlayer.WPF.Commands.CurrentSongBar
{
    public class PreviousSongCommand : BaseCommand
    {
        private ISoundPlayerManager _soundPlayerManager;

        public PreviousSongCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public override void Execute(object parameter)
        {
            _soundPlayerManager.PlayPrevious();
        }
    }
}
