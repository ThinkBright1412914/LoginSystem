using LoginSystem.Client.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Client.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly SessionService _sessionService;

        public UserProfileViewComponent(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var session = _sessionService.GetUserSession();
            if (session != null)
            {
                return View(session);
            }
            else
            {
                return View(string.Empty);
            }
        }
    }
}
