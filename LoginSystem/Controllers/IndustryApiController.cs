using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryApiController : ControllerBase
    {
        private readonly IIndustry _industry;

        public IndustryApiController(IIndustry industry)
        {
            _industry = industry;
        }

        [HttpGet("GetIndustry")]
        public async Task<IActionResult> GetIndustry()
        {
            var response = await _industry.GetIndutries();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateIndustry")]
        public async Task<IActionResult> CreateIndustry(IndustryDto request)
        {
            var response = await _industry.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteIndustry")]
        public async Task<IActionResult> DeleteIndustry(int Id)
        {
            var response = await _industry.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
