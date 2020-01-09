using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;
using Skinshare.Data.Interfaces;

namespace Skinshare.Data.Services
{
    public class RoutineService : IRoutineService
    {
        private readonly RoutineContext _routineContext;

        public RoutineService(RoutineContext routineContext)
        {
            _routineContext = routineContext;
        }
        
        public async Task<Routine> GetRoutine(string identifier)
        {
            var result = await _routineContext.Routines.Include(r => r.Steps).AsNoTracking().FirstOrDefaultAsync(r => r.Identifier == identifier);
            return result;
        }
    }
}