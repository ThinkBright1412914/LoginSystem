using LoginSystem.Client.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
	public class BookingController : Controller
	{
		private readonly IMovieRequest _movieRequest;

		public BookingController (IMovieRequest movieRequest)
		{
			_movieRequest = movieRequest;
		}


		public async Task<IActionResult> BookNow(int Id)
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> MovieInfo(int Id)
		{
			var movieDetails = await _movieRequest.GetMovieById(Id);
			if(movieDetails != null)
			{
				return View(movieDetails);
			}
			else
			{
				return NotFound();
			}			
		}
	}
}
