using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;
using Skinshare.Data;
using Skinshare.Data.Interfaces;
using Skinshare.Web.Pages;

namespace Skinshare.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : ControllerBase
    {
        private readonly IRoutineService _routineService;

        public RoutinesController(RoutineContext context, IRoutineService routineService)
        {
            _routineService = routineService;
        }

        // GET: api/Routines
        [HttpGet]
        public async Task<IEnumerable<Routine>> GetRoutines()
        {
            return await _routineService.ListAsync();
        }

        // GET: api/Routines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Routine>> GetRoutine(int id)
        {
            var routine = await _routineService.FindAsync(id);

            if (routine == null)
            {
                return NotFound();
            }

            return routine;
        }

        // PUT: api/Routines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoutine(int id, Routine routine)
        {
            if (id != routine.Id)
            {
                return BadRequest();
            }

            try
            {
                await _routineService.UpdateAsync(routine);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _routineService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Routines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Routine>> PostRoutine(Routine routine)
        {
            await _routineService.AddAsync(routine);

            return CreatedAtAction("GetRoutine", new { id = routine.Id }, routine);
        }

        // DELETE: api/Routines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Routine>> DeleteRoutine(int id)
        {
            var routine = await _routineService.FindAsync(id);
            if (routine == null)
            {
                return NotFound();
            }

            await _routineService.DeleteAsync(routine);

            return routine;
        }
    }
}
