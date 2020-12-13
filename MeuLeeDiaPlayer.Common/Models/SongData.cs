namespace MeuLeeDiaPlayer.Common.Models
{
    public class SongData
    {

        public Song Song { get; }
        public bool MarksStartOfPlaylist { get; }

        public SongData(Song song, bool marksStartOfPlaylist)
        {
            Song = song;
            MarksStartOfPlaylist = marksStartOfPlaylist;
        }
    }
}
