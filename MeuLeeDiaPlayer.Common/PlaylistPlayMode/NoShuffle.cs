﻿using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.Common.Models;
using System.Linq;

namespace MeuLeeDiaPlayer.Common.PlaylistPlayMode
{
    public class NoShuffle : PlayMode
    {
        public NoShuffle(LoopStyle loopStyle) : base(loopStyle)
        { }

        public override SongData GetNextSong(Playlist playlist)
        {
            _ = playlist ?? throw new System.ArgumentNullException(nameof(playlist));
            return GetNextSong(playlist, false);
        }

        private SongData GetNextSong(Playlist playlist, bool marksStartOfPlaylist)
        {
            if (playlist.Songs.IsEmpty()) return null;
            Song song = null;

            var nextSong = playlist.Songs.FirstOrDefault(s => s.Value == 0).Key;

            if (nextSong == null && LoopStyle != LoopStyle.NoLoop)
            {
                playlist.ResetSongsCounter();
                return GetNextSong(playlist, true);
            }

            if (LoopStyle == LoopStyle.LoopSong)
            {
                song = playlist.MarkSongToBePlayed(playlist.CurrentSong ??= nextSong);
                return new SongData(song, marksStartOfPlaylist);
            }

            song = playlist.MarkSongToBePlayed(nextSong); // looping is disabled. this value can be null or not
            return new SongData(song, marksStartOfPlaylist);
        }

        public override string ToString()
        {
            return $"NoShuffle | {LoopStyle}";
        }
    }
}
