using LoginSystem.Client.Models;
using LoginSystem.Client.Service;
using LoginSystem.Client.Service.Interfaces;
using LoginSystem.Utility;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoginSystem.Client.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SessionService _sessionService;
        private readonly UserService _userService;
        private readonly ICarouselRequest _carousel;
        private readonly IMovieRequest _movieReq;

        public HomeController(ILogger<HomeController> logger, SessionService sessionService, 
            UserService userService, ICarouselRequest carousel , IMovieRequest movieReq)
        {
            _logger = logger;
            _sessionService = sessionService;
            _userService = userService;
            _carousel = carousel;
            _movieReq = movieReq;
		}

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                HomeViewModel model = new HomeViewModel();
                var carousel = await _carousel.GetAll();
                var premierMovie = await _movieReq.GetMovies(UserConstant.PremierMovie);
                var futureMovie = await _movieReq.GetMovies(UserConstant.UpcomingMovie);
                model.Carousels = carousel.Any() ? carousel : new List<CarouselDto>();  
				model.PremierMovieList = premierMovie.Any() ? premierMovie : new List<MovieDto>();
				model.UpcomingMovieList = futureMovie.Any() ? futureMovie : new List<MovieDto>();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Authenticate");
            }

        }

        [HttpGet]
        public IActionResult ProfileInfo()
        {
            var userInfo = _sessionService.GetUserSession();
            return View(userInfo);
        }

        public IActionResult LogOut()
        {
            _sessionService.LogOut();
            return RedirectToAction("Index", "Authenticate");

        }

        [HttpGet]
		public async Task<IActionResult> EditUser()
		{
			var result = _sessionService.GetUserSession();
            UserVM model = new UserVM()
            {
                UserId = result.UserId,
                UserName = result.UserName,
                Email = result.Email,
                ActivationCode = result.ActivationCode,
                ExpirationDate = result.ExpirationDate,
                Message = "Request",
                ImageData = result.ImageData 
            };

            UserImageVM userVM = new UserImageVM()
            {
                User = model
            };
            return View(userVM);
		}

		[HttpPost]
		public async Task<IActionResult> EditUser(UserImageVM model)
		{
			if (model != null)
			{
                if(model.Image != null)
                {
					var img = await new Converter().ConvertToBase64(model.Image);
					model.User.ImageData = img;
				}
           
                var result = await _userService.EditUser(model.User);
				if (result != null)
				{
                    _sessionService.SetUserSession(result);
					TempData["success"] = "User updated successfully";
					return RedirectToAction("Index");

				}
			}
			return View(model);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
