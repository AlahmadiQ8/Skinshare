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
    public class IndexModel : PageModel
    {
        private readonly Skinshare.Data.RoutineContext _context;

        public IndexModel(Skinshare.Data.RoutineContext context)
        {
            _context = context;
        }

        public IList<Routine> Routine { get;set; }

        public async Task OnGetAsync()
        {
            Routine = await _context.Routines.AsNoTracking().ToListAsync();
        }
    }
}
