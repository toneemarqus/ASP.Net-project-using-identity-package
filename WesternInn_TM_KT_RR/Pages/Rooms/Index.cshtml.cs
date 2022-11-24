using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_TM_KT_RR.Data;
using WesternInn_TM_KT_RR.Model;

namespace WesternInn_TM_KT_RR.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Room != null)
            {
                Room = await _context.Room.ToListAsync();
            }
        }
    }
}
