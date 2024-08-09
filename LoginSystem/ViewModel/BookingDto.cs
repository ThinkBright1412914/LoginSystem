using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginSystem.ViewModel
{
	public class BookingDto
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public int ShowId { get; set; }

		public int NoOfTicket { get; set; }
		public IEnumerable<SelectListItem> ShowList { get; set; }

		public string SeatDetails { get; set; }
		public DateTime BookingDate { get;set; }

		public Decimal TotalAmount { get; set; }
	}
}
