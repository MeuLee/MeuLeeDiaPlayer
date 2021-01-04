using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SoundPlayer;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public class PlayPlaylistCommand : BaseCommand
    {
        private readonly ISoundPlayerManager _soundPlayerManager;
        private readonly IPlaylistHolder _playlistHolder;

        public PlayPlaylistCommand(ISoundPlayerManager soundPlayerManager, IPlaylistHolder playlistHolder)
        {
            _soundPlayerManager = soundPlayerManager;
            _playlistHolder = playlistHolder;
        }

        public override void Execute(object parameter)
        {
            bool arePlaylistsDifferent = _playlistHolder.SoundPlaylist != _playlistHolder.UIPlaylist;
            if (_soundPlayerManager.CurrentPlaylist is null || arePlaylistsDifferent)
            {
                _playlistHolder.SoundPlaylist = _playlistHolder.UIPlaylist;
            }

            if (_soundPlayerManager.CurrentSong is null || arePlaylistsDifferent)
            {
                _soundPlayerManager.PlayCurrentPlaylist();
            }
            else
            {
                _soundPlayerManager.PauseOrResume();
            }
        }
    }
}
