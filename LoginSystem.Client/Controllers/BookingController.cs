using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
	public class BookingController : Controller
	{
		private readonly IBookingRequest _bookingRequest;

		public BookingController (IBookingRequest bookingRequest)
		{
			_bookingRequest = bookingRequest;
		}

		public async Task<IActionResult> BookNow(int movieId)
		{
			var response = await _bookingRequest.GetBookingOption(movieId);
			if(response.Message != SystemMessage.ShowNotFound)
			{
				return View(response);
			}
			else
			{
				TempData["error"]= SystemMessage.ShowNotFound;	
				return RedirectToAction("Index","Home");
			}
			
		}

		//[HttpGet]
		//public async Task<IActionResult> MovieInfo(int Id)
		//{
		//	var movieDetails = await _movieRequest.GetMovieById(Id);
		//	if(movieDetails != null)
		//	{
		//		return View(movieDetails);
		//	}
		//	else
		//	{
		//		return NotFound();
		//	}			
		//}
	}
}
