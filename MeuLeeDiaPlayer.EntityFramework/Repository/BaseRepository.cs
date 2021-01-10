using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public abstract class BaseRepository<T> : IModelRepository<T> where T : DbModel
    {
        protected readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<T> CreateAsync(T entity)
        {
            var entryResult = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entryResult.Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
            if (entity == null) return null;
            var entryResult = _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entryResult.Entity;
        }

        public abstract Task<IEnumerable<T>> GetAsync();

        public abstract Task<T> GetAsync(int id);

        public async Task<T> UpdateAsync(T entity)
        {
            var result = _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        protected async Task<IEnumerable<T>> BaseGetAsync<TProperty>(
            Expression<Func<T, List<PlaylistSong>>> include,
            Expression<Func<PlaylistSong, TProperty>> thenInclude)
        {
            return await _dbContext
                .Set<T>()
                .Include(include)
                .ThenInclude(thenInclude)
                .ToListAsync();
        }

        protected async Task<T> BaseGetAsync<TProperty>(
            Expression<Func<T, List<PlaylistSong>>> include,
            Expression<Func<PlaylistSong, TProperty>> thenInclude,
            int id)
        {
            return await _dbContext
                .Set<T>()
                .Include(include)
                .ThenInclude(thenInclude)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
