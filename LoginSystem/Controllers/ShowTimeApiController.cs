using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimeApiController : ControllerBase
    {
        private readonly IShowTime _showTime;

        public ShowTimeApiController(IShowTime showTime)
        {
            _showTime = showTime;
        }

        [HttpGet("GetShowTime")]
        public async Task<IActionResult> Get()
        {
            var response = await _showTime.GetShowTime();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("Create-ShowTime")]
        public async Task<IActionResult> Create(ShowTimeDto request)
        {
            var response = await _showTime.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("Delete-ShowTime")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _showTime.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
