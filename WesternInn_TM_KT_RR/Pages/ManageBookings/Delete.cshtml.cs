﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_TM_KT_RR.Data;
using WesternInn_TM_KT_RR.Model;

namespace WesternInn_TM_KT_RR.Pages.ManageBookings
{
    public class DeleteModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public DeleteModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }
            var booking = await _context.Booking.FindAsync(id);

            if (booking != null)
            {
                Booking = booking;
                _context.Booking.Remove(Booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
