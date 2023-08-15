using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.UserViewModels
{
    public class ChangePasswordVm
    {
        
        [Required(ErrorMessage = "Current Password is required")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string? ConfirmPassword { get; set; }
    }
}