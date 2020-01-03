using System;
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
    public class EditModel : PageModel
    {
        private readonly Skinshare.Data.RoutineContext _context;

        public EditModel(Skinshare.Data.RoutineContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Routine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutineExists(Routine.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoutineExists(int id)
        {
            return _context.Routines.Any(e => e.Id == id);
        }
    }
}
