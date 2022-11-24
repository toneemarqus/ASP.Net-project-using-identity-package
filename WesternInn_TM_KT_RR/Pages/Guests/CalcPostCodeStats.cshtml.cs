using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_TM_KT_RR.models;
using WesternInn_TM_KT_RR.Models;

namespace WesternInn_TM_KT_RR.Pages.Guests
{
    [Authorize(Roles = "administrators'")]
    public class CalcPostCodeStatsModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public CalcPostCodeStatsModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // For passing the results to the Content file
        public IList<PostCodeStats> PostCodeStatistics { get; set; } = default!;
        public IList<RoomBookingStats> RoomBookingStatistics { get; set; } = default!;

        // GET: /Movies/CalcGenreStats
        public async Task<IActionResult> OnGetAsync()
        {
            // divide the movies into groups by genre
            var postCodeGroups = _context.Guest.GroupBy(m => m.postCode);

            // for each group, get its genre value and the number of movies in this group
            PostCodeStatistics = await postCodeGroups
                             .Select(g => new PostCodeStats { postCode = g.Key, NumOfGuests = g.Count() })
                             .ToListAsync();

            var roomGroups = _context.Booking.GroupBy(m => m.RoomID);

            RoomBookingStatistics = await roomGroups
                                .Select(x => new RoomBookingStats { RoomID = x.Key, NumOfBookings = x.Count() })
                                .ToListAsync();

            return Page();
        }

        //public void OnGet()
        //{
        //}
    }




}
