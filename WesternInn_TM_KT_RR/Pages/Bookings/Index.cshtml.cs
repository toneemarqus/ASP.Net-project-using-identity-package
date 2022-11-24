using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WesternInn_TM_KT_RR.Data;
using WesternInn_TM_KT_RR.Model;

namespace WesternInn_TM_KT_RR.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync(string? sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                // When the Index page is loaded for the first time, the sortOrder is empty.
                // By default, the movies should be displayed in the order of title_asc.
                sortOrder = "CheckIn_asc";
            }
            var orders = (IQueryable<Booking>)_context.Booking;
            switch (sortOrder)
            {
                case "CheckIn_asc":
                    orders = orders.OrderBy(m => m.CheckIn);
                    break;
                case "CheckIn_desc":
                    orders = orders.OrderByDescending(m => m.CheckIn);
                    break;
                case "TotalPrice_asc":
                    orders = orders.OrderBy(m => (double)m.TotalPrice);
                    break;
                case "TotalPrice_desc":
                    orders = orders.OrderByDescending(m => (double)m.TotalPrice);
                    break;
               
            }
            ViewData["NextCheckIn"] = sortOrder != "CheckIn_asc" ? "CheckIn_asc" : "CheckIn_desc";
            ViewData["NextTotalPriceOrder"] = sortOrder != "TotalPrice_asc" ? "TotalPrice_asc" : "TotalPrice_desc";

            Booking = await orders.Include(b => b.TheGuest)
                .Include(b => b.TheRoom).AsNoTracking().ToListAsync();
            
        }
    }
}
