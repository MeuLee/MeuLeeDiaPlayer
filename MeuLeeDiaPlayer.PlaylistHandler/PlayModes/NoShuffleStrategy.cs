using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Utils;
using System;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlayModes
{
    public class NoShuffleStrategy : PlayMode
    {
        public NoShuffleStrategy(LoopStyle loopStyle) : base(loopStyle) { }

        public override SongDto GetNextSong(PlaylistLoopInfo playlist)
        {
            _ = playlist ?? throw new ArgumentNullException(nameof(playlist));

            if (playlist.Songs.IsEmpty()) return SetLastSongPlayed(playlist, null);

            SongDto song;

            var nextSong = playlist.GetNextSongNotPlayedYet();

            if (nextSong is null && LoopStyle != LoopStyle.NoLoop)
            {
                playlist.ResetSongsCounter();
                return GetNextSong(playlist);
            }

            if (LoopStyle == LoopStyle.LoopSong)
            {
                song = playlist.MarkSongToBePlayed(playlist.LastSongPlayed ??= nextSong);
                return song;
            }

            song = playlist.MarkSongToBePlayed(nextSong); // looping is disabled. this value can be null or not
            return SetLastSongPlayed(playlist, song);
        }

        public override string ToString()
        {
            return $"NoShuffle | {LoopStyle}";
        }
    }
}
