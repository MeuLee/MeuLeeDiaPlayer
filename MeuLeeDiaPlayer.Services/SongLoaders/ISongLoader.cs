using MeuLeeDiaPlayer.Common.Models;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Services.SongLoaders
{
    public interface ISongLoader
    {
        List<Song> DbSongs { get; }
        Task LoadSongsAsync(ICollection<PlaylistDto> playlists);
        void MapSongs(PlaylistDto playlist);
        List<SongDto> Songs { get; }
        Task AddSongAsync(Song song);
    }
}
