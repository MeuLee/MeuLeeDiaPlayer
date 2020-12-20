using MeuLeeDiaPlayer.Common;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class PlaylistLoopInfo
    {
        public Dictionary<Song, int> Songs { get; private set; }

        public Song LoopedSong { get; set; }

        public PlaylistLoopInfo(Playlist playlist)
        {
            Songs = playlist.Songs.ToDictionary(0);
        }

        public void ResetSongsCounter()
        {
            Songs = Songs.ToDictionary(s => s.Key, s => 0);
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
    }
}
