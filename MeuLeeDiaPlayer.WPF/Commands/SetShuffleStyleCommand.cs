using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using System;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class SetShuffleStyleCommand : BaseCommand
    {
        private readonly ISoundPlayerManager _soundPlayerManager;

        public SetShuffleStyleCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public override void Execute(object parameter)
        {
            _soundPlayerManager.ShuffleStyle = _soundPlayerManager.ShuffleStyle switch
            {
                ShuffleStyle.NoShuffle => ShuffleStyle.Shuffle,
                ShuffleStyle.Shuffle => ShuffleStyle.NoShuffle,
                _ => throw new NotImplementedException()
            };
        }
    }
}
