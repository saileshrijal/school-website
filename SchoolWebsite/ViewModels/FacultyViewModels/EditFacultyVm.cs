using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.FacultyViewModels
{
    public class EditFacultyVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Faculty Name is required")]
        public string? Name { get; set; }
    }
}