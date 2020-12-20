﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.EntityFramework.Repository
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> UpdateAsync(int id, T entity);
    }
}
