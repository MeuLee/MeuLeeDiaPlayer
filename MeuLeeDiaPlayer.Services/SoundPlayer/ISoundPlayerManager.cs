using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;

namespace MeuLeeDiaPlayer.Services.SoundPlayer
{
    public interface ISoundPlayerManager
    {
        int Volume { get; set; }
        SongDto CurrentSong { get; }
        bool Stopped { get; }

        ShuffleStyle ShuffleStyle { get; set; }
        LoopStyle LoopStyle { get; set; }
        PlaylistDto CurrentPlaylist { get; }

        void ChangePlaylist(PlaylistDto playlist);
        void PlaySong(SongDto song);
        void PlayCurrentPlaylist();
        void PauseOrResume();
        void PlayCurrent();
        void PlayNext();
        void PlayPrevious();
    }
}