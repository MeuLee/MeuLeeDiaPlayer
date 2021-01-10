using Meziantou.Framework.WPF.Collections;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class PlaylistDto : ObservableObject
    {
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ConcurrentObservableCollection<SongDto> Songs { get; set; }

        private string _name;

        public void OnModifiedPlaylistList()
        {
            OnPropertyChanged(nameof(Songs));
        }
    }
}
