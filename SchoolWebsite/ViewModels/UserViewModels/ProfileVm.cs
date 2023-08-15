using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.UserViewModels
{
    public class ProfileVm
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Primary Contact is required")]
        public string? PrimaryContact { get; set; }
        public string? SecondaryContact { get; set; }
        public string? TemporaryAddress { get; set; }
        [Required(ErrorMessage = "Permanent Address is required")]
        public string? PermanentAddress { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}