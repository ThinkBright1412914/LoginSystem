using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginSystem.ViewModel
{
	public class BookingDto
	{
		public int Id { get; set; }
		public Guid? UserId { get; set; }
		public int ShowId { get; set; }
		public int? NoOfTicket { get; set; }
		public string? Location { get; set; }
		public string? SeatDetails { get; set; }
		public string? TicketPrice { get; set; }
		public DateTime BookingDate { get;set; }
		public Decimal TotalAmount { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> ShowTimeList { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> CinemaList { get; set; }
		public string? Message { get; set; }	
	}
}
