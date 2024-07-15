using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenre _genre;

        public GenreController(IGenre genre)
        {
            _genre = genre;
        }

        [HttpGet("GetGenres")]
        public async Task<IActionResult> GetGenres()
        {
            var response = await _genre.GetGenres();
            if (response.Count() > 0)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateGenre")]
        public async Task<IActionResult> CreateGenre(GenreDto request)
        {
            var response = await _genre.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteGenre")]
        public async Task<IActionResult> DeleteGenre(int Id)
        {
            var response = await _genre.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
