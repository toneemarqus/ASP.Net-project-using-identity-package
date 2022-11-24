using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WesternInn_TM_KT_RR.models;
using WesternInn_TM_KT_RR.Model;

namespace WesternInn_TM_KT_RR.Pages.Rooms
{
    public class searchModel : PageModel
    {
        private readonly WesternInn_TM_KT_RR.Data.ApplicationDbContext _context;

        public searchModel(WesternInn_TM_KT_RR.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SearchRooms RoomsInput { get; set; }

        public IList<Room> DiffRooms { get; set; }
        public List<SelectListItem> rooms { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime send1 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime send2 { get; set; }
        public string stime { get; set; }
        public string stime2 { get; set; }

        public IActionResult OnGet()
        {
            rooms = new List<SelectListItem> {
        new SelectListItem { Value = "1", Text = "1" },
        new SelectListItem { Value = "2", Text = "2" },
        new SelectListItem { Value = "3", Text = "3" },

    };
            // and save them in ViewData for passing to Content file
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
        // prepare the parameters to be inserted into the query
            var beds = new SqliteParameter("bedcount", RoomsInput.beds);
            var indate = new SqliteParameter("date1", RoomsInput.CheckIn);
            var outdate = new SqliteParameter("date2", RoomsInput.CheckOut);
            send1 = RoomsInput.CheckIn;
            send2 = RoomsInput.CheckOut;
            stime= send1.ToString("dd/MM/yyyy");
            stime2= send2.ToString("dd/MM/yyyy");
            // Use placeholders as the parameters
            var diffRooms = _context.Room.FromSqlRaw("select [Room].* from [Room]  where [Room].BedCount == @bedcount and [Room].ID"
                                                   + " NOT in (select [Room].ID from [Room] inner join [Booking] on"
                                                   + "[Room].ID == [Booking].RoomID and " +
                                                   "@date1<= [Booking].Checkout and @date2 >= [Booking].Checkout) ", beds, indate, outdate);

            // Run the query and save the results in DiffRooms for passing to content file
            DiffRooms = await diffRooms.ToListAsync();
            // Save the options for both dropdown lists in ViewData for passing to content file
            // invoke the content file
            
                rooms = new List<SelectListItem> {
        new SelectListItem { Value = "1", Text = "1" },
        new SelectListItem { Value = "2", Text = "2" },
        new SelectListItem { Value = "3", Text = "3" },

    };
                return Page();
        }

       
    }
}
