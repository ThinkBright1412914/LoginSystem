using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRequest _genre;

        public GenreController(IGenreRequest genre)
        {
            _genre = genre;
        }

        public async Task<IActionResult> GetGenres()
        {
            var response = await _genre.GetAll();
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

		[HttpPost]
        public async Task<IActionResult> Create(string name)
		{
            var response = await _genre.Create(new GenreDto { Name = name });
			if (response != null)
			{
				return Json(new { message = response.Message,});
			}
			else
			{
				return Json(new { success = false });
			}          
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _genre.Delete(Id);
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
