using LoginSystem.Client.Models;
using LoginSystem.Utility;
using LoginSystem.ViewModel;

namespace LoginSystem.Client.Service
{
    public class SessionService
    {
        private readonly HttpContext _context;
        
        public UserVM getUserSession()
        {
            var userInfo = _context.Session.getObjectFromJson<UserVM>("UserInfo");
            return userInfo;
        }

        public void setUserSession(UserVM model)
        {
            _context.Session.setObjectAsJson("UserInfo", model);
        }

        public void logOut()
        {
            _context.Session.Remove("UserInfo");
        }
    }
}
