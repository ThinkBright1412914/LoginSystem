using LoginSystem.Domain.Model;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginSystem.CustomMapper
{
	public static class BookingCustomMapper
	{
		public static List<BookingDto> ToBookingModel(List<Show> dbShows)
		{
			return dbShows.Select(show => new BookingDto
			{
				ShowId = show.Id,
				TicketPrice = show.TicketPrice.ToString(),
				ShowTimeList = new List<SelectListItem> { new SelectListItem { Text = show.ShowTimeInfo.Time } },
				CinemaList = new List<SelectListItem> { new SelectListItem { Text = show.CinemaInfo.Name } }
			}).ToList();
		}
	}
}
