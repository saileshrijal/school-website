using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.GalleryImageViewModels
{
    public class EditGalleryImageVm
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Please select a gallery")]
        public int GalleryId { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        public IFormFile? Image { get; set; }

        public List<SelectListItem>? GallerySelectList { get; set; }
    }
}