using LoginSystem.Client.Models;
using LoginSystem.Utility;
using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public UserVM GetUserSession()
        {
            var userInfo = HttpContext?.Session.getObjectFromJson<UserVM>("UserInfo");
            return userInfo;
        }

        public void SetUserSession(UserVM model)
        {
            HttpContext?.Session.setObjectAsJson("UserInfo", model);
        }

        public void LogOut()
        {
            HttpContext.Session.Remove("UserInfo");
        }
    }

}
