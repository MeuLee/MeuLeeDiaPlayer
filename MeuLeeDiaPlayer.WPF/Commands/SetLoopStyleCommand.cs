using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using System;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class SetLoopStyleCommand : BaseCommand
    {
        private readonly ISoundPlayerManager _soundPlayerManager;

        public SetLoopStyleCommand(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public override void Execute(object parameter)
        {
            _soundPlayerManager.LoopStyle = _soundPlayerManager.LoopStyle switch
            {
                LoopStyle.NoLoop => LoopStyle.LoopPlaylist,
                LoopStyle.LoopPlaylist => LoopStyle.LoopSong,
                LoopStyle.LoopSong => LoopStyle.NoLoop,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
