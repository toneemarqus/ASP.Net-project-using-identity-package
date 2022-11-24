using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WesternInn_TM_KT_RR.Data;
using WesternInn_TM_KT_RR.Model;
using WesternInn_TM_KT_RR.models;
namespace WesternInn_TM_KT_RR.Pages.Bookings
{
    public class CreateModel : PageModel
    {

        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public CreateModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Booking Booking { get; set; }
        public Room Room { get; set; }
        public int ID { get; set; }
        public string Indate { get; set; }
        public string Outdate { get; set; }

        public IList<Room> DiffRooms { get; set; }

        public IActionResult OnGet(int id, string indate, string outdate)
        {
            ID = id;
            Indate = indate;
            Outdate = outdate;
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "ID");

            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["RoomID"] = new SelectList(_context.Set<Room>(), "ID", "ID");
            if (!ModelState.IsValid || _context.Booking == null || Booking == null)
            {
                return Page();
            }

          
                var emptyBooking = new Booking();

                /* emptyOrder: the object to update using the values submitted.
                 * "Order": the Prefix used in the input names in web form.
                 * Lambda expression: list the properties to be updated. If not listed, 
                 *                    no updates, thus preventing overposting.
                 */
                var success = await TryUpdateModelAsync<Booking>(emptyBooking, "Booking",
                                    s => s.ID, s => s.RoomID, s => s.GuestEmail, s => s.CheckIn, s => s.CheckOut, s => s.TotalPrice);

            var beds = new SqliteParameter("ID", Booking.RoomID);
            var indate = new SqliteParameter("date1", Booking.CheckIn);
            var outdate = new SqliteParameter("date2", Booking.CheckOut);
         
            // Use placeholders as the parameters
            var diffRooms = _context.Room.FromSqlRaw("select [Room].* from [Room]  where [Room].ID == @ID and [Room].ID"
                                                   + "  in (select [Room].ID from [Room] inner join [Booking] on"
                                                   + "[Room].ID == [Booking].RoomID and " +
                                                   "@date1<= [Booking].Checkout and @date2 >= [Booking].Checkout) ", beds, indate, outdate);

            // Run the query and save the results in DiffRooms for passing to content file
            DiffRooms = await diffRooms.ToListAsync();
            if (success && DiffRooms.Count == 0)
                {
                    // Calcualte days 
                    // Obtain user input on the date 
                    DateTime start = emptyBooking.CheckIn;
                    // Obtain user input 
                    DateTime end = emptyBooking.CheckOut;
                    TimeSpan datediff = (end - start);
                    int TotalDays = (int)(end - start).TotalDays;
                    // Get Room by Room ID 
                    var theRoom = await _context.Room.FindAsync(emptyBooking.RoomID);
                    // calculate the total Price for the room
                    emptyBooking.TotalPrice = TotalDays * theRoom.Price;
                    // add this newly-created order into db
                    _context.Booking.Add(emptyBooking);
                    await _context.SaveChangesAsync();
                    // View Data 
                    ViewData["TotalPrice"] = TotalDays * theRoom.Price;
                return Page();
            }
            else
            {
                return Page();
            }

           
        }
    }
}
