using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
    public class CarouselController : Controller
    {
        private readonly ICarouselRequest _carousel;

        public CarouselController(ICarouselRequest carousel)
        {
            _carousel = carousel;
        }

        
        public async Task<IActionResult> GetCarousels()
        {
            var response = await _carousel.GetAll();
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            if (file != null)
            {
				var imgData = await new Converter().ConvertToBase64(file);
                CarouselDto model = new()
                {
                    Image = imgData
                };
				var response = await _carousel.Create(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetCarousels");
            }
            else
            {
                TempData["error"] = "Please upload an Image!";
                return View();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _carousel.Delete(Id);
            if (response != null)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
