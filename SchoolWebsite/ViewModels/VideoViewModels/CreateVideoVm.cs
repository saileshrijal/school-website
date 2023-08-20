using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.VideoViewModels
{
    public class CreateVideoVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Video Url is required")]
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }
    }
}