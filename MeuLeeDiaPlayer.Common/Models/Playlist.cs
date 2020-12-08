using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class Playlist
    {
        public Dictionary<Song, int> Songs { get; private set; }

        public Song CurrentSong { get; set; }

        public string PlaylistName
        {
            get => _playlistName;
            set => _playlistName = value ?? $"Playlist {UnnamedPlaylistIndex}";
        }

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

        internal Song MarkSongToBePlayed(Song song)
        {
            if (song != null)
            {
                Songs[song]++;
            }
            return song;
        }

        internal List<Song> GetSongsNotPlayedYet()
        {
            return Songs
                .Where(s => s.Value == 0)
                .Select(s => s.Key)
                .ToList();
        }
    }
}
