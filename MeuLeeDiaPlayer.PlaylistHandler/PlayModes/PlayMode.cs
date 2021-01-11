using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.Models;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlayModes
{
    public abstract class PlayMode
    {
        protected LoopStyle LoopStyle { get; }

        public static PlayMode GetPlayMode(ShuffleStyle shuffleStyle, LoopStyle loopStyle)
        {
#pragma warning disable CS8524
            return shuffleStyle switch
#pragma warning restore CS8524
            {
                ShuffleStyle.NoShuffle => new NoShuffleStrategy(loopStyle),
                ShuffleStyle.Shuffle => new ShuffleStrategy(loopStyle)
            };
        }

        protected PlayMode(LoopStyle loopStyle)
            => LoopStyle = loopStyle;

        public abstract SongDto GetNextSong(PlaylistLoopInfo playlist);

        protected static SongDto SetLastSongPlayed(PlaylistLoopInfo playlist, SongDto song)
        {
            playlist.LastSongPlayed = song;
            return song;
        }
    }
}
