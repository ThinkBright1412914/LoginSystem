using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselController : ControllerBase
    {
        private readonly ICarousel _carousel;

        public CarouselController(ICarousel carousel)
        {
            _carousel = carousel;
        }

        [HttpGet("GetCarousels")]
        public async Task<IActionResult> GetCarousels()
        {
            var response = await _carousel.GetCarousels();
            if (response.Count() > 0)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("CreateCarousel")]
        public async Task<IActionResult> CreateCarousel(CarouselDto request)
        {
            var response = await _carousel.Create(request);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteCarousel")]
        public async Task<IActionResult> DeleteCarousel(int Id)
        {
            var response = await _carousel.Delete(Id);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
