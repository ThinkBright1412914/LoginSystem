using LoginSystem.Client.Service.Interfaces;
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

	}
}
