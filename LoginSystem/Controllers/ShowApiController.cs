using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowApiController : ControllerBase
    {
        private readonly IShow _show;

        public ShowApiController(IShow show)
        {
            _show = show;   
        }

        [HttpGet("GetShows")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _show.GetShows();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetShowById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _show.GetShowById(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }


        [HttpPost("Create-Show")]
        public async Task<IActionResult> Create(ShowDto request)
        {
            var response = await _show.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("Update-Show")]
        public async Task<IActionResult> Update(ShowDto request)
        {
            var response = await _show.Update(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("Delete-Show")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _show.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
