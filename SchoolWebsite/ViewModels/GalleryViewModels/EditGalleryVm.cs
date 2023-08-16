using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.GalleryViewModels
{
    public class EditGalleryVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name for the gallery.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}