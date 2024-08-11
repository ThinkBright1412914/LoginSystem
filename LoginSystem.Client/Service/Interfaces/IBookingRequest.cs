using DocumentFormat.OpenXml.Office2010.Excel;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Org.BouncyCastle.Asn1.Mozilla;

namespace LoginSystem.Client.Service.Interfaces
{
	public interface IBookingRequest	
	{
		Task<BookingDto> GetBookingOption(int movieId);
		Task<BookingDto> GetOptionByShowId(int Id);
	}

	public class BookingRequest : IBookingRequest
	{
		private readonly IhttpService _httpService;
		public BookingRequest(IhttpService httpService)
		{
			_httpService = httpService;
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
	}
}
