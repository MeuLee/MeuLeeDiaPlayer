using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.Common.Models;
using System;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode
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

        public abstract SongData GetNextSong(Playlist playlist);

        protected PlayMode(LoopStyle loopStyle)
            => LoopStyle = loopStyle;
    }
}
