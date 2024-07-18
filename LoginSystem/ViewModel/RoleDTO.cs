using System.ComponentModel.DataAnnotations;

namespace LoginSystem.ViewModel
{
    public class RoleDTO
    {
        public Guid? RoleId { get; set; }
        [Required]
        public string? RoleName { get; set; }

        public string? Message { get; set; } 
    }
}
