using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Domain.Model
{
    public class UserInfo
    {
        [Key]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool IsActive { get; set; }

        public string? ActivationCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public byte[]? ImageFile { get; set; }
        public bool IsForcePasswordReset { get; set; }

        public ICollection<Role> Roles { get; set; }   
        
        public ICollection<UserRole> UserRoles { get; set; }    



    }
}
