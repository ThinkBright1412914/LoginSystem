using LoginSystem.Model;

namespace LoginSystem.ViewModel
{
    public class AuthenticationDTO
    {
        public string Token { get; set; }

        public UserInfo User { get; set; }  
    }
}
