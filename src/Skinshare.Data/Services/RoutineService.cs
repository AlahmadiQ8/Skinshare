using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Routine> FindAsync(string identifier)
        {
            var result = await _routineContext.Routines.Include(r => r.Steps).AsNoTracking().FirstOrDefaultAsync(r => r.Identifier == identifier);
            return result;
        }

        public async Task<Routine> FindAsync(int id)
        {
            var result = await _routineContext.Routines
                .Include(r => r.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
                
            return result;
        }

        public async Task<IEnumerable<Routine>> ListAsync()
        {
            var result = await _routineContext.Routines.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<bool> Exists(int id)
        {
            return await _routineContext.Routines.AnyAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Routine entity)
        {
            _routineContext.Entry(entity).State = EntityState.Modified;
            await _routineContext.SaveChangesAsync();
        }

        public async Task<Routine> AddAsync(Routine routine)
        {
            await _routineContext.AddAsync(routine);
            var i = await _routineContext.SaveChangesAsync();
            if (i == 0)
            {
                throw new Exception($"Failed to create {typeof(Routine)}");
            }

            return routine;
        }

        public async Task DeleteAsync(Routine routine)
        {
            _routineContext.Remove(routine);
            await _routineContext.SaveChangesAsync();
        }
    }
}