using MeuLeeDiaPlayer.Common.Models;

namespace MeuLeeDiaPlayer.PlaylistHandler.Models
{
    public class SongData
    {
        public SongDto Song { get; }
        public bool MarksStartOfPlaylist { get; }

        public SongData(SongDto song, bool marksStartOfPlaylist)
        {
            Song = song;
            MarksStartOfPlaylist = marksStartOfPlaylist;
        }
    }
}
