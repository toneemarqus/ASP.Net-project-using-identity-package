using System.ComponentModel.DataAnnotations;

namespace WesternInn_TM_KT_RR.Model
{
    public class Room
    {
        [Key]
        // ID is auto increment 
        public int ID { get; set; }

        [Required(ErrorMessage = "Floor level required.")]
        // Expression for G, following one numeric number
        [RegularExpression(@"^[G1-9]{1}", ErrorMessage = "Enter G following a number")]
        public string Level { get; set; } = string.Empty;

        [Required(ErrorMessage = "Number of beds in room required.")]
        // Range for bed count, between 1 - 10 
        [Range(1, 10)]
        public string BedCount { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Range(50, 300)]
        public decimal Price { get; set; }

        // Navigation properties
        public ICollection<Booking>? TheBooking { get; set; }

    }
}
