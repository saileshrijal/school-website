using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.EventViewModels
{
    public class EditEventVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Organizer is required")]
        public string? Organizer { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}