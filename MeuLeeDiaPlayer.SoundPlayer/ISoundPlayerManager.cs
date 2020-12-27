using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;

namespace MeuLeeDiaPlayer.SoundPlayer
{
    public interface ISoundPlayerManager
    {
        float Volume { get; set; }
        SongDto CurrentSong { get; }
        bool Stopped { get; }

        void ChangePlaylist(PlaylistDto playlist);
        void ChangePlayMode(ShuffleStyle shuffleStyle, LoopStyle loopStyle);
        void Play(SongDto song);
        void PauseOrResume();
        void PlayCurrent();
        void PlayNext();
        void PlayPrevious();
    }
}