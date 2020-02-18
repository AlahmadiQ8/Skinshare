using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using Skinshare.Core.Entities;
using Skinshare.Data;
using Skinshare.Data.Interfaces;
using Skinshare.Web.Contracts.Requests;
using Skinshare.Web.Contracts.Responses;
using Skinshare.Web.Pages;

namespace Skinshare.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : ControllerBase
    {
        private readonly IRoutineService _routineService;
        private readonly IMapper _mapper;

        public RoutinesController(RoutineContext context, IRoutineService routineService, IMapper mapper)
        {
            _routineService = routineService;
            _mapper = mapper;
        }

        // GET: api/Routines/5
        [HttpGet("{identifier}")]
        public async Task<ActionResult<RoutineResponse>> GetRoutine(string identifier)
        {
            var routine = await _routineService.FindAsync(identifier);

            if (routine == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RoutineResponse>(routine));
        }

        // POST: api/Routines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        // TODO: [FromBody] is required so Nswag produce correct httpclient post request 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RoutineResponse>> PostRoutine([FromBody] RoutineRequest routine)
        {
            var res = await _routineService.AddAsync(new Routine
            {
                Title = routine.Title, 
                Description = routine.Description, 
                Steps = routine.Steps.Select(s => new Step
                {
                    Description = s.Description,
                    Order = s.Order,
                    PartOfDay = s.PartOfDay
                }).ToList()
            });

            return CreatedAtAction("GetRoutine", new { id = res.Id }, _mapper.Map<RoutineResponse>(res));
        }
    }
}
