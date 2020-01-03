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
    public class DeleteModel : PageModel
    {
        private readonly Skinshare.Data.RoutineContext _context;

        public DeleteModel(Skinshare.Data.RoutineContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Routine Routine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Routine = await _context.Routines.FirstOrDefaultAsync(m => m.Id == id);

            if (Routine == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Routine = await _context.Routines.FindAsync(id);

            if (Routine != null)
            {
                _context.Routines.Remove(Routine);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
