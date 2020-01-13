using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skinshare.Core.Entities;

namespace Skinshare.Data.Interfaces
{
    public interface IRoutineService
    {
        Task<Routine> FindAsync(string identifier);
        Task<Routine> FindAsync(int id);
        Task<IEnumerable<Routine>> ListAsync();
        Task<bool> Exists(int id);
        Task UpdateAsync(Routine entity);
        Task<Routine> AddAsync(Routine routine);
        Task DeleteAsync(Routine routine);
    }
}