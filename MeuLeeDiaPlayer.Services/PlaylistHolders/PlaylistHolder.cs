using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.Services.SoundPlayer;
using Meziantou.Framework.WPF.Collections;
using System;
using System.Linq;

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
        
        public void RemoveSong(int songId)
        {
            RemoveSongFromAllPlaylists(songId);
            RemoveSongFromUIPlaylist(songId);
            RemoveSongFromSoundPlaylist(songId);
        }

        private void RemoveSongFromAllPlaylists(int songId)
        {
            foreach (var playlist in Playlists)
            {
                var song = playlist.Songs.FirstOrDefault(s => s.Id == songId);
                if (song == null) continue;
                playlist.Songs.Remove(song);
            }
            OnPropertyChanged(nameof(Playlists));
        }

        private void RemoveSongFromUIPlaylist(int songId)
        {
            var song = UIPlaylist?.Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null) return;
            UIPlaylist.Songs.Remove(song);
            OnPropertyChanged(nameof(UIPlaylist));
        }

        private void RemoveSongFromSoundPlaylist(int songId)
        {
            var song = SoundPlaylist?.Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null) return;
            SoundPlaylist.Songs.Remove(song);
            OnPropertyChanged(nameof(SoundPlaylist));
        }
    }
}
