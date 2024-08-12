using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LoginSystem.Domain.Model
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey(nameof(UserInfo))]
		public Guid UserId { get; set; }

		[ForeignKey(nameof(Show))]
		public int ShowId { get; set; }
		public string SeatDetails { get; set; }
		public int No_of_Tickets { get; set; }
		public DateTime Date { get; set; }	
		public double TotalAmount { get; set; }
		public UserInfo User { get; set; }
		public Show ShowInfo { get; set; }

	}
}
