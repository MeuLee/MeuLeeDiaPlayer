using System.Collections.Generic;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class PlaylistDto : ObservableObject
    {
        public int Id { get; set; }

        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                _playlistName = value;
                OnPropertyChanged(nameof(PlaylistName));
            }
        }

        public List<SongDto> Songs { get; set; }

        private string _playlistName;
    }
}
