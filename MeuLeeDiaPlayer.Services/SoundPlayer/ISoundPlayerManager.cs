using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;

namespace MeuLeeDiaPlayer.Services.SoundPlayer
{
    public interface ISoundPlayerManager
    {
        float Volume { get; set; }
        SongDto CurrentSong { get; }
        bool Stopped { get; }

        void ChangePlaylist(PlaylistDto playlist);
        void ChangePlayMode(ShuffleStyle shuffleStyle, LoopStyle loopStyle);
        void PlaySong(SongDto song);
        void PlayCurrentPlaylist();
        void PauseOrResume();
        void PlayCurrent();
        void PlayNext();
        void PlayPrevious();
    }
}