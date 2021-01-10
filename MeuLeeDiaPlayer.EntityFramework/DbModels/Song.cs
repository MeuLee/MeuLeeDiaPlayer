using System.Collections.Generic;

namespace MeuLeeDiaPlayer.EntityFramework.DbModels
{
    public class Song : DbModel
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Path { get; set; }
        public List<PlaylistSong> PlaylistSongs { get; set; }
    }
}
