using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginSystem.Client.Controllers
{
    public class ShowController : Controller
    {
        private readonly IShowRequest _showRequest;
        private readonly ICinemaRequest _cinemaRequest;
        private readonly IShowTimeRequest _showTimeRequest;
        private readonly IMovieRequest _movieRequest;

        public ShowController(IShowRequest showRequest,ICinemaRequest cinemaRequest,
            IShowTimeRequest showTimeRequest, IMovieRequest movieRequest)
        {
            _showRequest = showRequest;
            _showTimeRequest = showTimeRequest;
            _movieRequest = movieRequest;
            _cinemaRequest = cinemaRequest;
        }
    
        public async Task<IActionResult> GetShows()
        {
            var response = await _showRequest.GetShow();
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetShowById(int Id)
        {
            var response = await _showRequest.GetShowById(Id);
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Creates()
        {
            var showTime = _showTimeRequest.GetAll().Result.Select(x =>
                                            new SelectListItem
                                            {
                                                Text = x.Time,
                                                Value = x.Id.ToString(),
                                            });
            var cinema = _cinemaRequest.GetCinemas().Result.Select(x =>
                                            new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.Id.ToString(),
                                            });
            var movie = _movieRequest.GetMovies(null).Result.Select(x =>
                                            new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.Id.ToString(),
                                            });

            ShowDto model = new()
            {
                CinemaList = cinema,
                ShowTimeList = showTime,
                MovieList = movie
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Creates(ShowDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _showRequest.Create(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetShows");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var response = await _showRequest.GetShowById(Id);
            response.CinemaList = _cinemaRequest.GetCinemas().Result.Select(x =>
                                            new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.Id.ToString(),
                                            });
            response.ShowTimeList = _showTimeRequest.GetAll().Result.Select(x =>
                                            new SelectListItem
                                            {
                                                Text = x.Time,
                                                Value = x.Id.ToString(),
                                            });
            response.MovieList = _movieRequest.GetMovies(null).Result.Select(x =>
                                            new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.Id.ToString(),
                                            });
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShowDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _showRequest.Update(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetShows");
            }
            else
            {
                return View(model);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _showRequest.Delete(Id);
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
