using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.CourseViewModels
{
    public class CreateCourseVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please select a faculty")]
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "Number of years is required")]
        public int NumberOfYears { get; set; }
        [Required(ErrorMessage = "Number of semesters is required")]
        public int NumberOfSem { get; set; }
        public string? Objectives { get; set; }
        public string? Scopes { get; set; }
        public string? AdmissionRequirement { get; set; }
        public string? CourseOfStudy { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? Syllabus { get; set; }
        public List<SelectListItem>? FacultySelectList { get; set; }
    }
}