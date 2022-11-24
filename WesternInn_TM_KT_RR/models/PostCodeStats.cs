using System.ComponentModel.DataAnnotations;

namespace WesternInn_TM_KT_RR.Models
{
	public class PostCodeStats
	{
		[Display(Name = "PostCode")]
		public string postCode { get; set; } = string.Empty;
		[Display(Name = "Number of Guests")]
		public int NumOfGuests { get; set; }
	}
}
