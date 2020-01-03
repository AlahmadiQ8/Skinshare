using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<IActionResult> OnGetAsync(string identifier)
        {
            Routine = await _context.Routines.AsNoTracking().FirstOrDefaultAsync(m => m.Identifier == identifier);

            if (Routine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
