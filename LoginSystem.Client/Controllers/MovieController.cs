using System.Globalization;
using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginSystem.Client.Controllers
{
	public class MovieController : Controller
	{
		private readonly IMovieRequest _movieReq;
		private readonly IGenreRequest _genre;
		private readonly ILanguageRequest _language;
		private readonly IIndustryRequest _industry;

		public MovieController(IMovieRequest movieReq, IGenreRequest genre,
			ILanguageRequest language, IIndustryRequest industry)
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
				response.ReleaseDate = new Converter().DateFormatter(response.ReleaseDate);
				return View(response);
			}
			else
			{
				return NotFound();
			}
		}

		public async Task<IActionResult> Create()
		{
			var genre = _genre.GetAll().Result.Select(x =>
											new SelectListItem
											{
												Text = x.Name,
												Value = x.Id.ToString(),
											});
			var language = _language.GetAll().Result.Select(x =>
											new SelectListItem
											{
												Text = x.Name,
												Value = x.Id.ToString(),
											});
			var industry = _industry.GetAll().Result.Select(x =>
											new SelectListItem
											{
												Text = x.Name,
												Value = x.Id.ToString(),
											});

			MovieDto model = new()
			{
				GenresList = genre,
				LanguageList = language,
				IndustryList = industry
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(IFormFile file, MovieDto model)
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
				return View();
			}
		}

		public async Task<IActionResult> Edit(int Id)
		{
			var response = await _movieReq.GetMovieById(Id);
			response.GenresList = _genre.GetAll().Result.Select(x =>
											new SelectListItem
											{
												Text = x.Name,
												Value = x.Id.ToString(),
											});
			response.LanguageList = _language.GetAll().Result.Select(x =>
											new SelectListItem
											{
												Text = x.Name,
												Value = x.Id.ToString(),
											});
			response.IndustryList = _industry.GetAll().Result.Select(x =>
											new SelectListItem
											{
												Text = x.Name,
												Value = x.Id.ToString(),
					
											});
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(IFormFile? file, MovieDto model)
		{
			if (ModelState.IsValid)
			{
				if(file != null)
				{
					var imgData = await new Converter().ConvertToBase64(file);
					model.Image = imgData;
				}				
				var response = await _movieReq.Update(model);
				TempData["success"] = response.Message;
				return RedirectToAction("GetMovies");
			}
			else
			{
				return View(model);
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
