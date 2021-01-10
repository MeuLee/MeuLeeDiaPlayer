using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public class PlaylistSongRepository : IPlaylistSongRepository
    {
        private readonly AppDbContext _dbContext;

        public PlaylistSongRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Playlist> AddPlaylistSongAsync(PlaylistSong playlistSong)
        {
            var playlist = await _dbContext.Playlists
                .Include(p => p.PlaylistSongs).ThenInclude(ps => ps.Song)
                .FirstOrDefaultAsync(p => p.Id == playlistSong.PlaylistId);

            if (playlist == null) return null;

            var song = await _dbContext.Songs
                .FirstOrDefaultAsync(s => s.Id == playlistSong.SongId);

            if (song == null) return null;

            var newPlaylistSong = new PlaylistSong
            {
                Playlist = playlist,
                Song = song
            };

            var entryResult = await _dbContext.PlaylistSongs.AddAsync(newPlaylistSong);
            await _dbContext.SaveChangesAsync();

            return entryResult.Entity.Playlist;
        }

        public async Task<Playlist> DeletePlaylistSongAsync(PlaylistSong playlistSong)
        {
            var result = await _dbContext
                .PlaylistSongs
                .FirstOrDefaultAsync(ps => ps.PlaylistId == playlistSong.PlaylistId
                    && ps.SongId == playlistSong.SongId);
            if (result == null) return null;

            var entryResult = _dbContext.PlaylistSongs.Remove(result);
            await _dbContext.SaveChangesAsync();
            return entryResult.Entity.Playlist;
        }
    }
}
