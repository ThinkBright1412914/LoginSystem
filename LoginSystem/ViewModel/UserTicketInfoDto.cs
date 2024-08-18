

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LoginSystem.ViewModel
{
	public class UserTicketInfoDto
	{
		public string Show { get; set; }
		public string Movie { get; set; }
		public string Date {  get; set; }	
		public string Time { get; set; }	
		public string SeatDetails { get; set; }
	}
}
