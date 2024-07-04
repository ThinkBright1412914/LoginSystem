using LoginSystem.Client.Models;
using LoginSystem.Utility;

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
			HttpContext.Session.Remove("token");
		}

		public void SetAuthenticationSession(string model)
		{
			HttpContext?.Session.setObjectAsJson("token", model);
		}

		public bool TokenExists(string Token)
		{
			var checkToken = HttpContext?.Session.getObjectFromJson<string>("token");
            if (checkToken != null)
            {
                return true;
            }
            return false;
		}

        public string GetToken()
        {
            var token = HttpContext?.Session.getObjectFromJson<string>("token");
            return token;               
        }

    }

}
