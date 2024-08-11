using LoginSystem.DTO;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Idenitity.Services
{
	public interface IBooking
	{
		Task<BookingDto> GetBookingOption(int movieId);
		Task<BookingDto> ConfirmBooking();
	}

	public class Bookings : IBooking
	{
		private readonly ApplicationDbContext _context;
		public Bookings(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BookingDto> GetBookingOption(int movieId)
		{
			var response = new BookingDto();
			var dbShows = _context.Shows.Where(x => x.MovieId == movieId)
							.Include(x => x.MovieInfo)
							.Include(x => x.ShowTimeInfo)
							.ToList();

			if (dbShows != null && dbShows.Any())
			{
				response.ShowList = dbShows.Select(show => new SelectListItem { Text = show.Name , Value = show.Id.ToString() }).ToList();
				response.ShowTimeList = dbShows.Select(show => new SelectListItem { Text = show.ShowTimeInfo.Time ,Value = show.ShowTimeInfo.Id.ToString() }).ToList();
			}
			else
			{
				response.Message = SystemMessage.ShowNotFound;
			}
			return response;
		}

		public Task<BookingDto> ConfirmBooking()
		{
			throw new NotImplementedException();
		}

		
	}
}
