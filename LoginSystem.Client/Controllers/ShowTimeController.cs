using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
	public class ShowTimeController : Controller
	{
		private readonly IShowTimeRequest _showTimeReq;

		public ShowTimeController(IShowTimeRequest showTimeReq)
		{
			_showTimeReq = showTimeReq;
		}

		public async Task<IActionResult> GetShowTime()
		{
			var response = await _showTimeReq.GetAll();
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
		public async Task<IActionResult> Create(string time)
		{
			var response = await _showTimeReq.Create(new ShowTimeDto { Time = time });
			if (response != null)
			{
				return Json(new { message = response.Message, });
			}
			else
			{
				return Json(new { success = false });
			}
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int Id)
		{
			var response = await _showTimeReq.Delete(Id);
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
