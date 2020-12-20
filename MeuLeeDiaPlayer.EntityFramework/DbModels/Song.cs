using MeuLeeDiaPlayer.EntityFramework.Audio;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.EntityFramework.DbModels
{
    public class Song : DbModel
    {
        public string SongName
        {
            get => _songName;
            set => _songName = string.IsNullOrWhiteSpace(value) ? "Unknown Song" : value;
        }

        public string ArtistName
        {
            get => _artistName;
            set => _artistName = string.IsNullOrWhiteSpace(value) ? "Unknown Song" : value;
        }

        public string Path { get; set; }
        public IAudioStream FileReader { get; set; }
        public List<Playlist> Playlists { get; set; } = new();

        private string _songName;
        private string _artistName;


        public override string ToString()
        {
            return $"{SongName} - {ArtistName}";
        }
    }
}
