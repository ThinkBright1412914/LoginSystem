using LoginSystem.Domain.Model;
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
		Task<BookingDto> GetOptionByShowId(int Id);
		Task<BookingDto> ConfirmBooking(BookingDto request);
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
				response.MovieName = dbShows.FirstOrDefault().MovieInfo.Name;
				response.ShowList = dbShows.Select(show => new SelectListItem { Text = show.Name, Value = show.Id.ToString() }).ToList();
				response.ShowTimeList = dbShows.Select(show => new SelectListItem { Text = show.ShowTimeInfo.Time, Value = show.ShowTimeInfo.Id.ToString() }).ToList();
			}
			else
			{
				response.Message = SystemMessage.ShowNotFound;
			}
			return response;
		}

		public async Task<BookingDto> GetOptionByShowId(int Id)
		{
			var response = new BookingDto();
			var dbShows = _context.Shows.FirstOrDefault(x => x.Id == Id);

			if (dbShows != null)
			{
				response.TicketPrice = dbShows.TicketPrice;
				response.ReserveSeats = dbShows.ReservedSeats;
			}
			else
			{
				response.Message = SystemMessage.ShowNotFound;
			}
			return response;
		}

		public async Task<BookingDto> ConfirmBooking(BookingDto request)
		{
			var response = new BookingDto();
			try
			{
				var dbShows = _context.Shows.FirstOrDefault(x => x.Id == request.ShowId);
				if (dbShows != null)
				{
					dbShows.ReservedSeats = request.ReserveSeats;
					dbShows.SeatNo = (int.Parse(dbShows.SeatNo) - request.NoOfTicket).ToString();
					_context.Shows.Update(dbShows);
				}

				Booking model = new()
				{
					UserId = request.UserId,
					ShowId = request.ShowId,
					No_of_Tickets = request.NoOfTicket,
					Date = DateTime.Parse(request.BookingDate),
					TotalAmount = request.TotalAmount,
					SeatDetails = request.SeatDetails
				};

				response.Message = SystemMessage.Success;
				_context.Bookings.Add(model);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				throw;
			}
			return response;
		}
	}
}
