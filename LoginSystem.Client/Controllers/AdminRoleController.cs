using LoginSystem.Client.Service.Interfaces;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace LoginSystem.Client.Controllers
{
	public class AdminRoleController : Controller
	{
		private readonly IRoleRequest _role;
		public AdminRoleController(IRoleRequest role)
		{
			_role = role;
		}

		public async Task<IActionResult> GetRoles()
		{
			var response = await _role.GetRoles();
			if(response != null)
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
		public async Task<IActionResult> Create(RoleDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _role.CreateByAdminRole(model);
				TempData["success"] = response.Message;
				return RedirectToAction("GetRoles");
			}
			else
			{
				return View(model);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid roleId)
		{
			var response = await _role.DeleteByAdminRole(roleId);
			if (response != null)
			{
				return Json(new { success = true});
			}
			else
			{
				return Json(new { success = false });
			}
		}

	}
}
