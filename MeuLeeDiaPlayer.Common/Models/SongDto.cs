using MeuLeeDiaPlayer.EntityFramework.Audio;
using System;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class SongDto : ObservableObject, IEquatable<SongDto>
    {
        public int Id { get; set; }
        public string LengthFormat => FileReader?.Stream.TotalTime.ToString(@"mm\:ss") ?? string.Empty;

        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                OnPropertyChanged(nameof(Artist));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(Title);
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

        private string _artist;
        private string _title;
        private string _path;
        private IAudioStream _fileReader;

        public override string ToString()
        {
            return $"{Artist} - {Title}";
        }

        public bool Equals(SongDto other)
        {
            return Id == other.Id
                && Artist == other.Artist
                && Title == other.Title
                && Path == other.Path
                && FileReader == other.FileReader;
        }
    }
}
