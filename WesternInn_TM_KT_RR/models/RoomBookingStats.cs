using System.ComponentModel.DataAnnotations;

namespace WesternInn_TM_KT_RR.models
{
    public class RoomBookingStats
    {
        [Display(Name = "Room ID")]
        public int RoomID { get; set; }

        [Display(Name = "Number of Bookings")]
        public int NumOfBookings { get; set; }
    }
}
