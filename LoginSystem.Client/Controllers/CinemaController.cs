using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaRequest _cinemaReq;

        public CinemaController(ICinemaRequest cinemaReq)
        {
            _cinemaReq = cinemaReq;
        }

        public async Task<IActionResult> GetCinemas()
        {
            var response = await _cinemaReq.GetCinemas(); ;
            if (response != null)
            {
                return View(response);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetById(int Id)
        {
            var response = await _cinemaReq.GetCinemaById(Id);
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
        public async Task<IActionResult> Create(CinemaDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _cinemaReq.Create(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetCinemas");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var response = await _cinemaReq.GetCinemaById(Id);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CinemaDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _cinemaReq.Update(model);
                TempData["success"] = response.Message;
                return RedirectToAction("GetCinemas");
            }
            else
            {
                return View(model);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _cinemaReq.Delete(Id);
            if (response.isSuccess)
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

