using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Models;
using MeuLeeDiaPlayer.PlaylistHandler.PlayModes;
using MeuLeeDiaPlayer.PlaylistHandler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.SongLists
{
    public class SongList : ISongList
    {
        #region Properties

        public PlayMode PlayMode
        {
            get => _playMode;
            set
            {
                _playMode = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public PlaylistDto Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value ?? throw new ArgumentNullException(nameof(value));
                _playlistLoopInfo = new PlaylistLoopInfo(_playlist);
                ResetState();
            }
        }

        public SongDto CurrentSong
        {
            get
            {
                if (!IsPlayingHistory && HasFollowingSongs || IsPlayingHistory && HasPreviousSongs)
                {
                    return _history[_currentSongIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsFirstSong => _currentSongIndex == 0;
        private bool IsPlayingHistory => _history.Count - 1 > _currentSongIndex;
        private bool HasPreviousSongs => _currentSongIndex >= 0;
        private bool HasFollowingSongs => _currentSongIndex < _history.Count;

        #endregion

        #region Fields

        private readonly List<SongDto> _history = new();
        private PlayMode _playMode;
        private PlaylistDto _playlist;
        private PlaylistLoopInfo _playlistLoopInfo;
        private int _currentSongIndex;

        #endregion

        public SongList MoveNext()
        {
            _history.AddIfNotNull(GetNextSong());

            if (_history.Count > _currentSongIndex)
            {
                _currentSongIndex++;
            }

            return this;
        }

        public SongList MovePrevious()
        {
            if (_currentSongIndex > -1)
            {
                _currentSongIndex--;
            }

            return this;
        }

        public void Play(SongDto song)
        {
            _ = song ?? throw new ArgumentNullException(nameof(song));
            if (song.FileReader is null) return;
            if (_playlistLoopInfo.Songs.Keys.NotContains(song)) return;
            if (_history.LastOrDefault() != song)
            {
                _history.Add(song);
            }
            _currentSongIndex = _history.Count - 1;
        }

        private void ResetState()
        {
            _history.Clear();
            _currentSongIndex = 0;
            _history.AddIfNotNull(GetNextSong());
        }

        private SongDto GetNextSong()
        {
            if (Playlist is null || PlayMode is null) return null;
            var song = PlayMode.GetNextSong(_playlistLoopInfo);
            return song;
        }
    }
}
