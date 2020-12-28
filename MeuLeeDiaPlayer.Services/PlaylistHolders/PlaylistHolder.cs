using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.Services.PlaylistHolders
{
    public class PlaylistHolder : ObservableObject, IPlaylistHolder
    {
        public PlaylistDto CurrentPlaylist
        {
            get => _currentPlaylist;
            set
            {
                _currentPlaylist = value;
                OnPropertyChanged(nameof(CurrentPlaylist));
                _soundPlayerManager.ChangePlaylist(CurrentPlaylist);
            }
        }

        public List<PlaylistDto> Playlists { get; set; }

        private PlaylistDto _currentPlaylist;
        private readonly ISoundPlayerManager _soundPlayerManager;

        public PlaylistHolder(ISoundPlayerManager soundPlayerManager)
        {
            _soundPlayerManager = soundPlayerManager;
        }
    }
}
