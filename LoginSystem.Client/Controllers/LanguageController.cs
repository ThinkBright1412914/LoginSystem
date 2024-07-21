using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
	public class LanguageController : Controller
	{
		private readonly ILanguageRequest _language;

		public LanguageController(ILanguageRequest language)
		{
			_language = language;	
		}

		public async Task<IActionResult> GetLanguages()
		{
			var response = await _language.GetAll();
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
			var response = await _language.Create(new LanguagugeDto { Name = name });
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
			var response = await _language.Delete(Id);
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
