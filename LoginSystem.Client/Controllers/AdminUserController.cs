using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly UserService _userService;

        public AdminUserController(UserService userService)
        {
            _userService = userService;
        }

       public async Task<IActionResult> GetUsers()
       {
            IEnumerable<UserVM> response = await _userService.GetUsers();
            if(response.Any())
            {
                return View(response);
            }
            return NotFound();
       }

		public async Task<IActionResult> Details(Guid Id)
		{
            var response = await _userService.GetUserById(Id);
			if (response != null)
			{
				return View(response);
			}
			return NotFound();
		}

		public IActionResult Create()
		{
            return View();
		}

        [HttpPost]
		public async Task<IActionResult> Create(UserVM model)
		{
            if (ModelState.IsValid)
            {
				var response =await _userService.CreateByAdminUser(model);
                if(response != null)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction("GetUsers");
                }             
            }
            return View(model);
			
		}

        public async Task<IActionResult> Edit(Guid Id)
        {
            var response = await _userService.GetUserById(Id);
            if (response != null)
            {
                return View(response);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserVM request)
        {
            var response = await _userService.UpdateByAdminUser(request);
            if (response != null)
            {
                TempData["success"] = response.Message;
                return RedirectToAction("GetUsers"); ;
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var response = await _userService.GetUserById(Id);
            if (response != null)
            {
                return View(response);
            }
            return NotFound();
        }

        public async Task<IActionResult> ConfirmDelete(UserVM model)
        {
            var response = await _userService.DeleteByAdminUser(model);
            if (response != null)
            {
                TempData["success"] = response.Message;
                return RedirectToAction("GetUsers");
            }
            return NotFound();
        }
    }
}
