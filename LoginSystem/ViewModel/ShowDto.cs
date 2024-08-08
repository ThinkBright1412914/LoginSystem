using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LoginSystem.ViewModel
{
    public class ShowDto
    {
        public int Id { get; set; } 
        public int MovieId { get;set; }
        public DateTime ShowDate { get; set; }
        public int ShowTimeId { get; set; }
        [Required]
        public string SeatNo { get; set; }
        public int CinemaId { get; set; }
        [Required]  
        public decimal TicketPrice { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MovieList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ShowTimeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CinemaList { get; set; }

        public string? Message { get; set; } 
    }
}
