using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public class SongRepository : BaseRepository<Song>
    {
        public SongRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<Song>> GetAsync()
        {
            return await BaseGetAsync(p => p.PlaylistSongs, ps => ps.Playlist);
        }

        public override async Task<Song> GetAsync(int id)
        {
            return await BaseGetAsync(p => p.PlaylistSongs, ps => ps.Playlist, id);
        }
    }
}
