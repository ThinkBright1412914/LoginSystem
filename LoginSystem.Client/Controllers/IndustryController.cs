using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
	public class IndustryController : Controller
	{
		private readonly IIndustryRequest _industry;

		public IndustryController(IIndustryRequest industry)
		{
			_industry = industry;
		}

		public async Task<IActionResult> GetIndustry()
		{
			var response = await _industry.GetAll();
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
			var response = await _industry.Create(new IndustryDto { Name = name });
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
			var response = await _industry.Delete(Id);
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
