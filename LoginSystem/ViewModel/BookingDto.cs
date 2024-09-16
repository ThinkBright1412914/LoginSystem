using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginSystem.ViewModel
{
	public class BookingDto
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public string? UserName { get; set; }
		public int ShowId { get; set; }
		public string? MovieName { get; set; }	
		public int NoOfTicket { get; set; }
		public string? SeatDetails { get; set; }
		public decimal TicketPrice { get; set; }
		public string? BookingDate { get;set; }
		public string? BookingStatus { get; set; }
		public string? ReserveSeats { get; set; }
		public Decimal TotalAmount { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem>? ShowTimeList { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem>? ShowList { get; set; }
		public string? Message { get; set; }
		public DateTime? BookingDateTime
		{
			get
			{
				if (DateTime.TryParse(BookingDate, out var date))
				{
					return date;
				}
				return null;
			}
			set
			{
				if (value.HasValue)
				{
					BookingDate = value.Value.ToString("yyyy-MM-dd");
				}
				else
				{
					BookingDate = null;
				}
			}
		}
	}
}
