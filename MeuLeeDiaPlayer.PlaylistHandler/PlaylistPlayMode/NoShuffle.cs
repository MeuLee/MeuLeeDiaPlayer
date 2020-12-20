using MeuLeeDiaPlayer.Common;
using MeuLeeDiaPlayer.Common.Enums;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode
{
    public class NoShuffle : PlayMode
    {
        public NoShuffle(LoopStyle loopStyle) : base(loopStyle)
        { }

        public override SongData GetNextSong(PlaylistLoopInfo playlist)
        {
            _ = playlist ?? throw new System.ArgumentNullException(nameof(playlist));
            return GetNextSong(playlist, false);
        }

        private SongData GetNextSong(PlaylistLoopInfo playlist, bool marksStartOfPlaylist)
        {
            if (LoopStyle != LoopStyle.LoopSong)
            {
                playlist.LoopedSong = null;
            }

            if (playlist.Songs.IsEmpty()) return new SongData(null, marksStartOfPlaylist);
            Song song;

            var nextSong = playlist.Songs.FirstOrDefault(s => s.Value == 0).Key;

            if (nextSong is null && LoopStyle != LoopStyle.NoLoop)
            {
                playlist.ResetSongsCounter();
                return GetNextSong(playlist, true);
            }

            if (LoopStyle == LoopStyle.LoopSong)
            {
                song = playlist.MarkSongToBePlayed(playlist.LoopedSong ??= nextSong);
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
