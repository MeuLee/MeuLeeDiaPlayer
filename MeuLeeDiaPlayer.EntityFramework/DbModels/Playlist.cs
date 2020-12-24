using System;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.EntityFramework.DbModels
{
    public class Playlist : DbModel
    {
        public string PlaylistName { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
