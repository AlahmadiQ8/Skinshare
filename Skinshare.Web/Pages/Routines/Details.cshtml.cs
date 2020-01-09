using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;
using Skinshare.Data;
using Skinshare.Data.Interfaces;

namespace Skinshare.Web.Pages.Routines
{
    public class DetailsModel : PageModel
    {
        private readonly IRoutineService _routineService;
        private readonly IStepService _stepService;

        public DetailsModel(RoutineContext context, IRoutineService routineService, IStepService stepService)
        {
            _routineService = routineService;
            _stepService = stepService;
        }

        public Routine Routine { get; set; }
        public IEnumerable<Step> MorningSteps { get; set; }
        public IEnumerable<Step> EveningSteps { get; set; }

        public async Task<IActionResult> OnGetAsync(string identifier)
        {
            Routine = await _routineService.GetRoutine(identifier);

            if (Routine == null)
            {
                return NotFound();
            }

            MorningSteps = _stepService.GetSteps(Routine, PartOfDay.Morning);

            EveningSteps = _stepService.GetSteps(Routine, PartOfDay.Evening);

            return Page();
        }
    }
}