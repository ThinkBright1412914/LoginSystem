using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieApiController : ControllerBase
	{
		private readonly IMovies _movie;

		public MovieApiController(IMovies movie)
		{
			_movie = movie;
		}

		[HttpGet("GetMovies")]
		public async Task<IActionResult> GetMovies()
		{
			var response = await _movie.GetMovies();
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}

		[HttpGet("GetMovieById")]
		public async Task<IActionResult> GetMovieById(int Id)
		{
			var response = await _movie.GetMovieById(Id);
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}

		[HttpPost("Create-Movie")]
		public async Task<IActionResult> CreateMovie(MovieDto request)
		{
			var response = await _movie.Create(request);
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}

		[HttpPut("Update-Movie")]
		public async Task<IActionResult> UpdateMovie(MovieDto request)
		{
			var response = await _movie.Update(request);
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}

		[HttpDelete("Delete-Movie")]
		public async Task<IActionResult> DeleteMovie(int Id)
		{
			var response = await _movie.Delete(Id);
			if (response != null)
			{
				return Ok(response);
			}
			return BadRequest();
		}
	}
}
