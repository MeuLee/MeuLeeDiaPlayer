using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public interface IModelRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> DeleteAsync(int id);

        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync();

        Task<T> UpdateAsync(T entity);
    }
}
