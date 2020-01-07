using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;
using Skinshare.Data;

namespace Skinshare.Web.Pages.Routines
{
    public class DetailsModel : PageModel
    {
        private readonly RoutineContext _context;

        public DetailsModel(RoutineContext context)
        {
            _context = context;
        }

        public Routine Routine { get; set; }
        public IEnumerable<Step> MorningSteps { get; set; }
        public IEnumerable<Step> EveningSteps { get; set; }

        public async Task<IActionResult> OnGetAsync(string identifier)
        {
            Routine = await _context.Routines.Include(r => r.Steps).AsNoTracking().FirstOrDefaultAsync(r => r.Identifier == identifier);

            if (Routine == null)
            {
                return NotFound();
            }

            MorningSteps = Routine.Steps.Where(s => s.PartOfDay == PartOfDay.Morning).Select(s =>
            {
                s.Order += 1;
                return s;
            }).OrderBy(s => s.Order);

            EveningSteps = Routine.Steps.Where(s => s.PartOfDay == PartOfDay.Evening).Select(s =>
            {
                s.Order += 1;
                return s;
            }).OrderBy(s => s.Order);

            return Page();
        }
    }
}
