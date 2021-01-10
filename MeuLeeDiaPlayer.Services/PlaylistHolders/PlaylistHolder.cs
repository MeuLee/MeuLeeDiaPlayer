using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using Meziantou.Framework.WPF.Collections;

namespace MeuLeeDiaPlayer.Services.PlaylistHolders
{
    public class PlaylistHolder : ObservableObject, IPlaylistHolder
    {
        public PlaylistDto SoundPlaylist
        {
            get => _soundPlaylist;
            set
            {
                _soundPlaylist = value;
                OnPropertyChanged(nameof(SoundPlaylist));
                _soundPlayerManager.ChangePlaylist(SoundPlaylist);
            }
        }

        public ConcurrentObservableCollection<PlaylistDto> Playlists { get; set; }
        public PlaylistDto UIPlaylist
        {
            get => _uiPlaylist;
            set
            {
                _uiPlaylist = value;
                OnPropertyChanged(nameof(UIPlaylist));
                UIPlaylist?.OnModifiedPlaylistList();
            }
        }

        private PlaylistDto _soundPlaylist;
        private PlaylistDto _uiPlaylist;
        private readonly ISoundPlayerManager _soundPlayerManager;

        public PlaylistHolder(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }

        public void OnModifiedPlaylistList()
        {
            OnPropertyChanged(nameof(Playlists));
        }
    }
}
