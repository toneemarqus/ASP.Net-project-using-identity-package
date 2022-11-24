using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_TM_KT_RR.Data;
using WesternInn_TM_KT_RR.Model;

namespace WesternInn_TM_KT_RR.Pages.Bookings
{
    public class DetailsModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public DetailsModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }
            else 
            {
                Booking = booking;
            }
            return Page();
        }
    }
}
