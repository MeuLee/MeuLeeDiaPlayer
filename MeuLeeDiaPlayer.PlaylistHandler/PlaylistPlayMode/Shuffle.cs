using MeuLeeDiaPlayer.Common;
using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.Common.Models;
using System;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode
{
    public class Shuffle : PlayMode
    {
        private static readonly Random _r = new();

        internal Shuffle(LoopStyle loopStyle) : base(loopStyle)
        { }

        public override SongData GetNextSong(Playlist playlist)
        {
            _ = playlist ?? throw new ArgumentNullException(nameof(playlist));
            return GetNextSong(playlist, false);
        }

        private SongData GetNextSong(Playlist playlist, bool marksStartOfPlaylist)
        {
            if (LoopStyle != LoopStyle.LoopSong)
            {
                playlist.LoopedSong = null;
            }

            if (playlist.Songs.IsEmpty()) return new SongData(null, marksStartOfPlaylist);

            var songsNotPlayedYet = playlist.GetSongsNotPlayedYet();

            if (LoopStyle == LoopStyle.LoopSong)
            {
                var song = playlist.MarkSongToBePlayed(
                    playlist.LoopedSong ??= songsNotPlayedYet.GetRandomValueInList(_r));
                return new SongData(song, marksStartOfPlaylist);
            }

            if (songsNotPlayedYet.Any())
            {
                var song = playlist.MarkSongToBePlayed(songsNotPlayedYet.GetRandomValueInList(_r));
                return new SongData(song, marksStartOfPlaylist);
            }

            if (LoopStyle == LoopStyle.LoopPlaylist)
            {
                playlist.ResetSongsCounter();
                return GetNextSong(playlist, true);
            }

            return new SongData(null, marksStartOfPlaylist); // every song in the playlist has been played once, and looping is disabled
        }

        public override string ToString()
        {
            return $"Shuffle | {LoopStyle}";
        }
    }
}
