using LoginSystem.Idenitity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingApiController : ControllerBase
	{
		private readonly IBooking _booking;

		public BookingApiController(IBooking booking)
		{
			_booking = booking;
		}

		[HttpGet("GetBookingOption")]
		public async Task<IActionResult> GetBookingOptions(int movieId)
		{
			var response = await _booking.GetBookingOption(movieId);
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}

		[HttpGet("GetOptionByShowId")]
		public async Task<IActionResult> GetOptionByShowId(int Id)
		{
			var response = await _booking.GetOptionByShowId(Id);
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}
	}
}
