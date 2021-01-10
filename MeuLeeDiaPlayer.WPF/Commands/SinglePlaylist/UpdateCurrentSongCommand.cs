using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.PlaylistHolders;
using MeuLeeDiaPlayer.Services.SoundPlayer;

namespace MeuLeeDiaPlayer.WPF.Commands.SinglePlaylist
{
    public class UpdateCurrentSongCommand : BaseCommand
    {
        private readonly ISoundPlayerManager _soundPlayerManager;
        private readonly IPlaylistHolder _playlistHolder;

        public UpdateCurrentSongCommand(ISoundPlayerManager soundPlayerManager, IPlaylistHolder playlistHolder)
        {
            _soundPlayerManager = soundPlayerManager;
            _playlistHolder = playlistHolder;
        }

        public override void Execute(object parameter)
        {
            if (parameter is not SongDto song) return;
            if (_playlistHolder.SoundPlaylist != _playlistHolder.UIPlaylist)
            {
                _playlistHolder.SoundPlaylist = _playlistHolder.UIPlaylist;
            }

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
