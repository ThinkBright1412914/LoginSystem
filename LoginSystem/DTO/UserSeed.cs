using LoginSystem.Domain.Model;
using LoginSystem.Utility;
using NETCore.Encrypt;

namespace LoginSystem.DTO
{
    public class UserSeed
    {
        public static  List<UserInfo> DefaultUser()
        {
            return new List<UserInfo>()
            {
                new UserInfo()
                {
                    UserId = new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                    UserName = "Admin",
                    Email = "nabinthekishor@gmail.com",
                    IsActive = true,
                    Password = EncryptProvider.Base64Encrypt("#Admin123"),
                    ActivationCode = "678999",
                    ExpirationDate = DateTime.Now.AddDays(1),
                },
            };
        }

        public static List<UserRole> DefaultUserRole()
        {
            return new List<UserRole>()
            {
                new UserRole()
                {
                    UserId = new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                    RoleId = new Guid(UserConstant.AdminRole)
                },
            };
        }
    }
}
