using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaApiController : ControllerBase
    {
        private readonly ICinemas _cinema;

        public CinemaApiController(ICinemas cinema)
        {
            _cinema = cinema;
        }

        [HttpGet("GetCinemas")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _cinema.GetCinemas();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetCinemaById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _cinema.GetCinemaById(Id);
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpPost("Create-Cinema")]
        public async Task<IActionResult> Create(CinemaDto request)
        {
            var response = await _cinema.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("Update-Cinema")]
        public async Task<IActionResult> Update(CinemaDto request)
        {
            var response = await _cinema.Update(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("Delete-Cinema")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _cinema.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
