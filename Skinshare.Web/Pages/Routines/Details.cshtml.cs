using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Skinshare.Core.Entities;
using Skinshare.Data;
using Skinshare.Data.Interfaces;

namespace Skinshare.Web.Pages.Routines
{
    public class DetailsModel : PageModel
    {
        private readonly IRoutineService _routineService;
        private readonly IStepService _stepService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(RoutineContext context, IRoutineService routineService, IStepService stepService, ILogger<DetailsModel> logger)
        {
            _routineService = routineService;
            _stepService = stepService;
            _logger = logger;
        }

        public Routine Routine { get; set; }
        public IEnumerable<Step> MorningSteps { get; set; }
        public IEnumerable<Step> EveningSteps { get; set; }

        public async Task<IActionResult> OnGetAsync(string identifier)
        {
            Routine = await _routineService.FindAsync(identifier);

            if (Routine == null)
            {
                return NotFound();
            }

            MorningSteps = _stepService.GetSteps(Routine, PartOfDay.Morning).ToList();
            EveningSteps = _stepService.GetSteps(Routine, PartOfDay.Evening).ToList();

            return Page();
        }
    }
}