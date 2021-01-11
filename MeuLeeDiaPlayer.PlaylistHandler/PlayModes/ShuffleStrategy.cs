using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Enums;
using MeuLeeDiaPlayer.PlaylistHandler.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Utils;
using System;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlayModes
{
    public class ShuffleStrategy : PlayMode
    {
        private readonly Random _r = new();

        internal ShuffleStrategy(LoopStyle loopStyle) : base(loopStyle) { }

        public override SongDto GetNextSong(PlaylistLoopInfo playlist)
        {
            _ = playlist ?? throw new ArgumentNullException(nameof(playlist));

            if (playlist.Songs.IsEmpty()) return SetLastSongPlayed(playlist, null);

            var songsNotPlayedYet = playlist
                .GetSongsNotPlayedYet()
                .Where(s => s != playlist.LastSongPlayed || playlist.Songs.Count == 1)
                .ToList();

            if (LoopStyle == LoopStyle.LoopSong)
            {
                var song = playlist.MarkSongToBePlayed(
                    playlist.LastSongPlayed ??= songsNotPlayedYet.GetRandomValueInList(_r));
                return song;
            }

            if (songsNotPlayedYet.Any())
            {
                var song = playlist.MarkSongToBePlayed(songsNotPlayedYet.GetRandomValueInList(_r));
                return SetLastSongPlayed(playlist, song);
            }

            if (LoopStyle == LoopStyle.LoopPlaylist)
            {
                playlist.ResetSongsCounter();
                return GetNextSong(playlist);
            }

            return SetLastSongPlayed(playlist, null); // every song in the playlist has been played once, and looping is disabled
        }

        public override string ToString()
        {
            return $"Shuffle | {LoopStyle}";
        }
    }
}
