using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<T> GetAsync<TInclude>(int id, Expression<Func<T, TInclude>> includeExpression);

        Task<List<T>> GetAllAsync<TInclude>(Expression<Func<T, TInclude>> includeExpression);

        Task<T> UpdateAsync(int id, T entity);
    }
}
