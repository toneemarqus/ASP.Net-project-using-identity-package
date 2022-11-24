using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WesternInn_TM_KT_RR.Data;
using WesternInn_TM_KT_RR.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.Sqlite;

namespace WesternInn_TM_KT_RR.Pages.ManageBookings
{
    [Authorize(Roles = "administrators'")]
    public class CreateModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public CreateModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string Indate { get; set; }
        public string Outdate { get; set; }

        public IList<Room> DiffRooms { get; set; }
        public IActionResult OnGet()
        {
        ViewData["GuestEmail"] = new SelectList(_context.Guest, "Email", "FullName");
        ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var beds = new SqliteParameter("ID", Booking.RoomID);
            var indate = new SqliteParameter("date1", Booking.CheckIn);
            var outdate = new SqliteParameter("date2", Booking.CheckOut);
            var diffRooms = _context.Room.FromSqlRaw("select [Room].* from [Room]  where [Room].ID == @ID and [Room].ID"
                                                   + "  in (select [Room].ID from [Room] inner join [Booking] on"
            + "[Room].ID == [Booking].RoomID and " +
                                                   "@date1<= [Booking].Checkout and @date2 >= [Booking].Checkout) ", beds, indate, outdate);

            // Run the query and save the results in DiffRooms for passing to content file
            DiffRooms = await diffRooms.ToListAsync();

            if (DiffRooms.Count == 0)
            {
                _context.Booking.Add(Booking);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                ViewData["GuestEmail"] = new SelectList(_context.Guest, "Email", "FullName");
                ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
                return Page();
            }
            
        }
    }
}
