using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;
using Skinshare.Data;

namespace Skinshare.Web.Pages.Generated
{
    public class DetailsModel : PageModel
    {
        private readonly Skinshare.Data.RoutineContext _context;

        public DetailsModel(Skinshare.Data.RoutineContext context)
        {
            _context = context;
        }

        public Routine Routine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO: Add asnottracking
            Routine = await _context.Routines.FirstOrDefaultAsync(m => m.Id == id);

            if (Routine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
