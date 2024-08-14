using System.Text;
using DocumentFormat.OpenXml.Office2010.Excel;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Mozilla;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IBookingRequest	
	{
		Task<BookingDto> GetBookingOption(int movieId);
		Task<BookingDto> GetOptionByShowId(int Id);
		Task<BookingDto> CreateBooking(BookingDto request);
	}

	public class BookingRequest : IBookingRequest
	{
		private readonly IhttpService _httpService;
		private readonly SessionService _session;
		public BookingRequest(IhttpService httpService, SessionService session)
		{
			_httpService = httpService;
			_session = session;
		}

		public async Task<BookingDto> GetBookingOption(int movieId)
		{
			var (status, response) = await _httpService.GetAsync<BookingDto>(ApiUri.GetBookingOption + "?movieId=" + movieId);
			return response;
		}

		public async Task<BookingDto> GetOptionByShowId(int Id)
		{
			var (status, response) = await _httpService.GetAsync<BookingDto>(ApiUri.GetOptionByShowId + "?Id=" + Id);
			return response;
		}

		public async Task<BookingDto> CreateBooking(BookingDto request)
		{
			var user = _session.GetUserSession();
			request.UserId = user.UserId;
			string json = JsonConvert.SerializeObject(request);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var (status, response) = await _httpService.PostAsync<BookingDto>(ApiUri.CreateBooking, content);
			return response;
		}

	}
}
