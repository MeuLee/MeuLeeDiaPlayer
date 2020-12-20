using MeuLeeDiaPlayer.EntityFramework.Context;
using MeuLeeDiaPlayer.EntityFramework.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : DbModel
    {
        protected MeuLeeDiaPlayerDbContext _dbContext;

        public GenericRepository(MeuLeeDiaPlayerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<T> CreateAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> GetAsync(int id)
            => await _dbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var result = _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
