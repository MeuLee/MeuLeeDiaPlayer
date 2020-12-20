using MeuLeeDiaPlayer.Common;
using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using MeuLeeDiaPlayer.PlaylistHandler.PlaylistPlayMode;
using System;
using System.Collections.Generic;

namespace MeuLeeDiaPlayer.PlaylistHandler
{
    // Meant to be used as a singleton with DI framework.
    public class SongList
    {
        #region Properties

        public IReadOnlyList<Song> FollowingSongs
            => _songs.GetRange(_currentSongIndex, Math.Min(_songs.Count - _currentSongIndex, _queueSize)).AsReadOnly();

        public PlayMode PlayMode
        {
            get => _playMode;
            set
            {
                _playMode = value ?? throw new ArgumentNullException(nameof(value));
                RefillSongQueue();
            }
        }

        public Playlist Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value ?? throw new ArgumentNullException(nameof(value));
                _playlistLoopInfo = new PlaylistLoopInfo(_playlist);
                ResetState();
                FillSongQueue();
            }
        }

        public Song CurrentSong
        {
            get
            {
                if (_currentSongIndex >= _songs.Count || _currentSongIndex < 0) return null;
                return _songs[_currentSongIndex];
            }
        }

        #endregion

        #region Fields

        private readonly List<Song> _songs = new();
        private readonly List<int> _loopStartIndexes = new List<int> { 0 };
        private PlayMode _playMode;
        private Playlist _playlist;
        private PlaylistLoopInfo _playlistLoopInfo;
        private const int _queueSize = 10;
        private int _currentSongIndex;

        #endregion

        public SongList(PlayMode playMode, Playlist playlist)
        {
            // intentionally not calling set, to execute the rest of the set methods
            _playMode = playMode ?? throw new ArgumentNullException(nameof(playMode));
            _playlist = playlist ?? throw new ArgumentNullException(nameof(playlist));
            _playlistLoopInfo = new PlaylistLoopInfo(playlist);
            FillSongQueue();
        }

        /// <summary>
        /// Tries to add a song using the properties PlayMode and Playlist.
        /// If it did add a song, increments the current song index by one.
        /// </summary>
        /// <returns>An instance of this class, allowing calls like .MoveNext().CurrentSong;</returns>
        public SongList MoveNext()
        {
            AddSongToPlaylist(_currentSongIndex + _queueSize);
            // PlayMode.GetNextSong may return null, so need to check if it did first
            if (_currentSongIndex < _songs.Count)
            {
                _currentSongIndex++;
            }

            return this;
        }

        /// <summary>
        /// If the current song isn't the first one, decrements the current song index by one.
        /// </summary>
        /// <returns>An instance of this class, allowing calls like .MoveNext().CurrentSong;</returns>
        public SongList MovePrevious()
        {
            if (_currentSongIndex >= 0)
            {
                _currentSongIndex--;
            }
            return this;
        }

        #region Private methods

        private void RefillSongQueue()
        {
            int index = _currentSongIndex + 1;
            RemoveSongsFromIndex(index);
            FillSongQueue();
        }

        private void RemoveSongsFromIndex(int index)
        {
            int count = _songs.Count - index;
            if (count <= 0) return;

            int biggestSmallerIndex = GetBiggestSmallerIndex(index);
            var songsToMarkAsPlayed = _songs.GetRange(biggestSmallerIndex, index - biggestSmallerIndex);
            ResetPlaylistStats(songsToMarkAsPlayed); // marks the songs that were actually played as played
            _songs.RemoveRange(index, count); 
            _loopStartIndexes.RemoveAll(i => i >= index);
        }

        // assumes _loopStartIndexes is ordered ascending
        private int GetBiggestSmallerIndex(int index)
        {
            for (int i = _loopStartIndexes.Count - 1; i <= 0; i--)
            {
                if (_loopStartIndexes[i] < index) return _loopStartIndexes[i];
            }
            return 0;
        }

        private void ResetPlaylistStats(List<Song> songsToMarkAsPlayed)
        {
            _playlistLoopInfo.ResetSongsCounter();
            foreach (var song in songsToMarkAsPlayed)
            {
                _playlistLoopInfo.MarkSongToBePlayed(song);
            }
        }

        private void FillSongQueue()
        {
            for (int i = 0; i < _queueSize && _currentSongIndex + _queueSize > _songs.Count; i++)
            {
                int index = _songs.IsEmpty() ? 0 : _songs.Count - 1;
                AddSongToPlaylist(index);
            }
        }

        private void AddSongToPlaylist(int currentIndex)
        {
            var songData = PlayMode.GetNextSong(_playlistLoopInfo);
            if (songData.MarksStartOfPlaylist && songData.Song is not null)
            {
                AddUniqueAscendingIndexToIndexList(currentIndex);
            }
            _songs.AddIfNotNull(songData.Song);
        }

        private void AddUniqueAscendingIndexToIndexList(int index)
        {
            if (_loopStartIndexes.IsEmpty())
            {
                _loopStartIndexes.Add(index);
                return;
            }

            int lastValue = _loopStartIndexes[^1];
            if (index > lastValue)
            {
                _loopStartIndexes.Add(index);
            }
        }

        private void ResetState()
        {
            _songs.Clear();
            _loopStartIndexes.Clear();
            _loopStartIndexes.Add(0);
            _currentSongIndex = 0;
            _playlistLoopInfo.ResetSongsCounter();
        }

        #endregion
    }
}
