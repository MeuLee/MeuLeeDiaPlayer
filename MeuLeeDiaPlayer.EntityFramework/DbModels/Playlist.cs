using System.Collections.Generic;

namespace MeuLeeDiaPlayer.EntityFramework.DbModels
{
    public class Playlist : DbModel
    {
        public string Name { get; set; }
        public List<PlaylistSong> PlaylistSongs { get; set; }
    }
}
