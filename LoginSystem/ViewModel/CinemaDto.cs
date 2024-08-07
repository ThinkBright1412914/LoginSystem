using System.ComponentModel.DataAnnotations;

namespace LoginSystem.ViewModel
{
    public class CinemaDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public string? City { get; set; }
        public string? Message { get; set; }
        public bool isSuccess { get; set; } 
    }
}
