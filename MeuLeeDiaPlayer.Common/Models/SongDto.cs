using MeuLeeDiaPlayer.EntityFramework.Audio;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class SongDto : ObservableObject
    {
        public int Id { get; set; }
        public List<PlaylistDto> Playlists { get; set; }
        public string LengthFormat => FileReader?.Stream.TotalTime.ToString(@"mm\:ss") ?? string.Empty;

        public string ArtistName
        {
            get => _artistName;
            set
            {
                _artistName = value;
                OnPropertyChanged(nameof(ArtistName));
            }
        }

        public string SongName
        {
            get => _songName;
            set
            {
                _songName = value;
                OnPropertyChanged(SongName);
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public IAudioStream FileReader
        {
            get => _fileReader;
            set
            {
                _fileReader = value;
                OnPropertyChanged(nameof(LengthFormat));
            }
        }

        private string _artistName;
        private string _songName;
        private string _path;
        private IAudioStream _fileReader;

        public override string ToString()
        {
            return $"{SongName} | {ArtistName}";
        }
    }
}
