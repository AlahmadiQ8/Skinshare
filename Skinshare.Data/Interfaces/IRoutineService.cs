using System.Threading.Tasks;
using Skinshare.Core.Entities;

namespace Skinshare.Data.Interfaces
{
    public interface IRoutineService
    {
        Task<Routine> GetRoutine(string identifier);
    }
}