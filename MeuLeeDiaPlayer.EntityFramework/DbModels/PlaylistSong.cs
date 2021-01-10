namespace MeuLeeDiaPlayer.EntityFramework.DbModels
{
    public class PlaylistSong
    {
        public int SongId { get; init; }
        public int PlaylistId { get; init; }
        public Playlist Playlist { get; set; }
        public Song Song { get; set; }
    }
}
