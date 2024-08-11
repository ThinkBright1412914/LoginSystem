using LoginSystem.CustomMapper;
using LoginSystem.DTO;
using LoginSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Idenitity.Services
{
	public interface IBooking
	{
		Task<List<BookingDto>> GetBookingOption(int movieId);
		Task<BookingDto> ConfirmBooking();
	}

	public class Bookings : IBooking
	{
		private readonly ApplicationDbContext _context;
		public Bookings(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<BookingDto>> GetBookingOption(int movieId)
		{
			var response = new List<BookingDto>();	
			var dbShows = _context.Shows.Where(x => x.MovieId == movieId)
										.Include(x => x.MovieInfo)
										.Include(x => x.CinemaInfo)
										.Include(x => x.ShowTimeInfo).ToList();
			if (dbShows != null)
			{
				response = BookingCustomMapper.ToBookingModel(dbShows);
			}
			else
			{
				response.FirstOrDefault().Message = "Show not found";
			}
			return response;
		}

		public Task<BookingDto> ConfirmBooking()
		{
			throw new NotImplementedException();
		}

		
	}
}
