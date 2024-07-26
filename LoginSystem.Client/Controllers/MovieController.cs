using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRequest _movieReq;
        private readonly IGenreRequest _genre;
        private readonly ILanguageRequest _language;
        private readonly IIndustryRequest _industry;

        public MovieController(IMovieRequest movieReq, IGenreRequest genre,
            ILanguageRequest language ,IIndustryRequest industry)
        {
            _movieReq = movieReq;
            _genre = genre;
            _language = language;
            _industry = industry;
        }


        public async Task<IActionResult> GetMovies()
        {
            var response = await _movieReq.GetMovies(null); ;
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetMoviesById(int Id)
        {
            var response = await _movieReq.GetMovieById(Id);
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
            var genre = await _genre.GetAll();
            var language = await _language.GetAll();
            var industry = await _industry.GetAll();

            MovieDto model = new()
            {
                GenresList = genre,
                LanguageList = language,
                IndustryList = industry
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file , MovieDto model)
        {
            if (file != null)
            {
                var imgData = await new Converter().ConvertToBase64(file);
                model.Image = imgData;
                var response = await _movieReq.Create(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetMovies");
            }
            else
            {
                TempData["error"] = "Please upload an Image!";
                return View();
            }
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile file, MovieDto model)
        {
            if (file != null)
            {
                var imgData = await new Converter().ConvertToBase64(file);
                model.Image = imgData;
                var response = await _movieReq.Create(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetMovies");
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
            var response = await _movieReq.Delete(Id);
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
