using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WesternInn_TM_KT_RR.Model
{
    public class Guest
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Please enter a valid Email Address.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public string Email { get; set; } = string.Empty;
        // First name
        [Required(ErrorMessage = "Family name is required.")]
        [Display(Name = "Family Name")]
        [RegularExpression(@"[a-zA-Z'-]{2,20}")]
        // below is only used if first letter needs to be a capital
        //[RegularExpression(@"[A-Z][a-z'-]{2,20}")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Given name is required.")]
        [Display(Name = "Given name")]
        //English letters, and apostrophe
        [RegularExpression(@"[a-zA-Z'-]{2,20}")]
        public string GivenName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[0-8]{1}[0-9]{3}$", ErrorMessage = "postCode must be a 4 digit number, and must not start with 9, only postCode in australia will be accepted")]
        public string postCode { get; set; } = string.Empty;
        //Navigation properties
        public ICollection<Booking>? TheBooking { get; set; }
        [NotMapped] // not mapping this property to database, but exist in memory
        public string FullName => $"{GivenName} {Surname}";
    }
}
