using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Skinshare.Core.Entities;
using Skinshare.Data;
using Skinshare.Data.Interfaces;
using Skinshare.Web.Contracts.Requests;
using Skinshare.Web.Contracts.Responses;
using Skinshare.Web.Pages;
using Skinshare.Web.Pages.Generated;
using ILogger = Microsoft.VisualStudio.Web.CodeGeneration.ILogger;

namespace Skinshare.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : ControllerBase
    {
        private readonly IRoutineService _routineService;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<RoutinesController> _logger; 

        public RoutinesController(RoutineContext context, IRoutineService routineService, IMapper mapper, LinkGenerator linkGenerator, ILogger<RoutinesController> logger)
        {
            _routineService = routineService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _logger = logger;
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

            var res = _mapper.Map<RoutineResponse>(routine);
            res.Href = _linkGenerator.GetPathByPage("/Routines/Details", null, new {identifier});
            return Ok(res);
        }

        // POST: api/Routines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        // TODO: [FromBody] is required so Nswag produce correct httpclient post request 
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, typeof(RoutineResponse))]
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
                }).ToList(),
            });
            
            var response = _mapper.Map<RoutineResponse>(res);
            response.Href = _linkGenerator.GetPathByPage("/Routines/Details", null, new {res.Identifier});

            return CreatedAtAction("GetRoutine", new { id = res.Id }, response);
        }
    }
}
