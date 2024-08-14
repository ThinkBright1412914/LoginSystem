using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
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

		//jquery call
		public async Task<JsonResult> GetOptionByShowId(int Id)
		{
			BookingDto model = new();
			try
			{
				model = await _bookingRequest.GetOptionByShowId(Id);
			}
			catch (Exception ex)
			{
				throw;
			}

			return Json(model);
		}

		[HttpPost]
		public async Task<IActionResult> BookNow(BookingDto request)
		{
			var response = await _bookingRequest.CreateBooking(request);
			if(response.Message == SystemMessage.Success)
			{
				TempData["success"] = SystemMessage.Success;
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
	}
}
