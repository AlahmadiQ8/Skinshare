using System.Collections.Generic;
using System.Threading.Tasks;
using Skinshare.Core.Interfaces;

namespace Skinshare.Data
{
    public interface IAsyncRepository<T> where T : IEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}