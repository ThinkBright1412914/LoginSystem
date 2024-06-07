using LoginSystem.Model;
using LoginSystem.Utility;
using NETCore.Encrypt;
using Org.BouncyCastle.Asn1.Ocsp;

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
    }
}
