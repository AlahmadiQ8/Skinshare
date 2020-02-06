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

        // GET: api/Routines/5
        [HttpGet("{identifier}")]
        public async Task<ActionResult<Routine>> GetRoutine(string identifier)
        {
            var routine = await _routineService.FindAsync(identifier);

            if (routine == null)
            {
                return NotFound();
            }

            return routine;
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
    }
}
