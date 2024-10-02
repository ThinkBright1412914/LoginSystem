using LoginSystem.CustomMapper;
using LoginSystem.Domain.Model;
using LoginSystem.DTO;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LoginSystem.Idenitity.Services
{
	public interface IBooking
	{
		Task<List<BookingDto>> GetAllBookings();
		Task<BookingDto> GetBookingOption(int movieId);
		Task<BookingDto> GetOptionByShowId(int Id);
		Task<BookingDto> ConfirmBooking(BookingDto request);
		Task<List<UserTicketInfoDto>> GetUserPurchasedTicket(Guid userId , bool isHistory);
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
					var newSeats = JsonConvert.DeserializeObject<List<int>>(request.ReserveSeats);
					var existingSeats = JsonConvert.DeserializeObject<List<int>>(dbShows.ReservedSeats);
					existingSeats.AddRange(newSeats);
					dbShows.ReservedSeats = JsonConvert.SerializeObject(existingSeats);
					dbShows.SeatNo = (int.Parse(dbShows.SeatNo) - request.NoOfTicket).ToString();
					_context.Shows.Update(dbShows);
				}
					
				var model = BookingMapper.ToEntity(request);
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

		public async Task<List<UserTicketInfoDto>> GetUserPurchasedTicket(Guid userId, bool isHistory)
		{
			List<UserTicketInfoDto> response = new List<UserTicketInfoDto>();
			var dbBookings = _context.Bookings.Where(x => x.UserId == userId)
											  .Include(x => x.ShowInfo).ToList();
			if (isHistory)
			{
				dbBookings = dbBookings.Where(x => x.Date <= DateTime.UtcNow).ToList();
			}
			else
			{
				dbBookings = dbBookings.Where(x => x.Date >= DateTime.UtcNow).ToList();
			}

			if (dbBookings.Any())
			{
				var userTicketInfoModel = new UserTicketInfoDto();
				try
				{
					foreach (var item in dbBookings)
					{
						var dbResult = GetShows(item.ShowId);
						userTicketInfoModel.BookingId = item.Id;
						userTicketInfoModel.Show = dbResult.Result.Show;
						userTicketInfoModel.Time = dbResult.Result.Time;
						userTicketInfoModel.Date = item.Date.ToString("MM/dd/yyyy");
						userTicketInfoModel.Movie = dbResult.Result.Movie;
						userTicketInfoModel.SeatDetails = item.SeatDetails;
						response.Add(userTicketInfoModel);
					}
				}
				catch (Exception ex)
				{
					throw;
				}
			}

			return response;
		}

		private async Task<UserTicketInfoDto> GetShows(int showId)
		{
			var result = new UserTicketInfoDto();
			try
			{
				var dbShow = await _context.Shows
										   .Where(x => x.Id == showId)
										   .Include(x => x.ShowTimeInfo)
										   .Include(x => x.MovieInfo)
										   .AsNoTracking()
										   .FirstOrDefaultAsync();

				if (dbShow != null)
				{
					result.Show = dbShow.Name;
					result.Time = dbShow.ShowTimeInfo.Time;
					result.Movie = dbShow.MovieInfo.Name;
				}
			}
			catch(Exception ex)
			{
				throw;
			}	
			return result;
		}

		public async Task<List<BookingDto>> GetAllBookings()
		{
			var result = new List<BookingDto>();
			try
			{
				var dbBookings = _context.Bookings.Include(x => x.ShowInfo)
												  .Include(x => x.User)
												  .AsEnumerable();
				
				foreach(var item in dbBookings)
				{
					var booking = BookingMapper.ToModel(item);
					result.Add(booking);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}


}
