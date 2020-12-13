using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class Playlist
    {
        #region Properties

        public Dictionary<Song, int> Songs { get; private set; }

        /// <summary>
        /// Only has a value when the PlayMode is LoopSong.
        /// </summary>
        public Song LoopedSong { get; set; }

        public string PlaylistName
        {
            get => _playlistName;
            set => _playlistName = value ?? $"Playlist {UnnamedPlaylistIndex}";
        }

        #endregion

        private static int UnnamedPlaylistIndex => ++_unnamedPlaylistIndex;
        private static int _unnamedPlaylistIndex = 0;
        private string _playlistName;

        #region Constructors

        public Playlist(List<Song> songs, string playlistName)
        {
            _ = songs ?? throw new ArgumentNullException(nameof(songs));

            Songs = songs.ToDictionary(0);
            PlaylistName = playlistName;
        }

        public Playlist(string folder, string playlistName) : this(Song.GetSongsFromFolder(folder).ToList(), playlistName)
        { }

        #endregion

        #region Public methods

        public void ResetSongsCounter()
        {
            Songs = Songs.ToDictionary(s => s.Key, s => 0);
        }

        public void ResetSongsCounter(List<Song> songs)
        {
            foreach (var song in songs)
            {
                if (Songs.ContainsKey(song))
                {
                    Songs[song] = 0;
                }
            }
        }

        public Song MarkSongToBePlayed(Song song)
        {
            if (song is not null)
            {
                Songs[song]++;
            }
            return song;
        }

        public List<Song> GetSongsNotPlayedYet()
        {
            return Songs
                .Where(s => s.Value == 0)
                .Select(s => s.Key)
                .ToList();
        }

        #endregion
    }
}
