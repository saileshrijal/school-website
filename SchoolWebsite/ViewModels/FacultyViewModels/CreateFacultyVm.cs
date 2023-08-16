using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.FacultyViewModels
{
    public class CreateFacultyVm
    {
        [Required(ErrorMessage = "Faculty Name is required")]
        public string? Name { get; set; }
    }
}