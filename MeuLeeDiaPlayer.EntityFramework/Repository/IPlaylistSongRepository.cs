using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public interface IPlaylistSongRepository
    {
        Task<Playlist> AddPlaylistSongAsync(PlaylistSong playlistSong);
        Task<Playlist> DeletePlaylistSongAsync(PlaylistSong playlistSong);
    }
}
