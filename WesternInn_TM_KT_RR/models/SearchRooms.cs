using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace WesternInn_TM_KT_RR.models
{
    public class SearchRooms
    {
        [Required(ErrorMessage = "Number of beds required.")]
        public int beds { get; set; }
        [Required(ErrorMessage = "Check In date required.")]
        // Display title of coloumn as check out data
        [Display(Name = "Check-In Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }
        // Check out
        [Required(ErrorMessage = "Check out date required.")]
        // Display title of coloumn as check out data
        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }
      


    }
}
