using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.PlaylistHandler.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.Models
{
    public class PlaylistLoopInfo
    {
        public Dictionary<SongDto, int> Songs { get; private set; }

        public SongDto LoopedSong { get; set; }

        public PlaylistLoopInfo(PlaylistDto playlist)
        {
            Songs = playlist.Songs.ToDictionary(0);
        }

        public void ResetSongsCounter()
        {
            Songs = Songs.ToDictionary(s => s.Key, s => 0);
        }

        public SongDto MarkSongToBePlayed(SongDto song)
        {
            if (song is not null)
            {
                Songs[song]++;
            }
            return song;
        }

        public List<SongDto> GetSongsNotPlayedYet()
        {
            return Songs
                .Where(s => s.Value == 0)
                .Select(s => s.Key)
                .ToList();
        }
    }
}
