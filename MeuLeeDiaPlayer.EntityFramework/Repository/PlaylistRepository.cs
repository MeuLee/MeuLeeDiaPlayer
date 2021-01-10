using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public class PlaylistRepository : BaseRepository<Playlist>
    {
        public PlaylistRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<Playlist>> GetAsync()
        {
            return await BaseGetAsync(p => p.PlaylistSongs, ps => ps.Song);
        }

        public override async Task<Playlist> GetAsync(int id)
        {
            return await BaseGetAsync(p => p.PlaylistSongs, ps => ps.Song, id);
        }
    }
}
