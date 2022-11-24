using System.ComponentModel.DataAnnotations;

namespace WesternInn_TM_KT_RR.Model
{
    public class Booking
    {
        [Key]
        // ID is auto increment 
        public int ID { get; set; }

        // foreign key
        public int RoomID { get; set; }
        // foreign key
        [Required]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;
        //Check in
        [Required(ErrorMessage = "Check in required.")]
        // Display title of coloumn as check in data
        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }
        // Check out
        [Required(ErrorMessage = "Check out required.")]
        // Display title of coloumn as check out data
        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }
        //Total Price
        [Range(0, 10000.0)]
        public decimal TotalPrice { get; set; }

        // Navigation properoties 
        public Room? TheRoom { get; set; }
        public Guest? TheGuest { get; set; }

        public int datediff { get; internal set; }

    }
}
