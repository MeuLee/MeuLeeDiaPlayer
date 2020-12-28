using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.Models;
using System;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlayModes
{
    public abstract class PlayMode
    {
        protected LoopStyle LoopStyle { get; }

        public static PlayMode GetPlayMode(ShuffleStyle shuffleStyle, LoopStyle loopStyle)
        {
            return shuffleStyle switch
            {
                ShuffleStyle.NoShuffle => new NoShuffle(loopStyle),
                ShuffleStyle.Shuffle => new Shuffle(loopStyle),
                _ => throw new ArgumentException("Bad enum value", nameof(shuffleStyle)),
            };
        }

        public abstract SongData GetNextSong(PlaylistLoopInfo playlist);

        protected PlayMode(LoopStyle loopStyle)
            => LoopStyle = loopStyle;
    }
}
