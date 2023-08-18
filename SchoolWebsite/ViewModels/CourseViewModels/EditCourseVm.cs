using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.CourseViewModels
{
    public class EditCourseVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int FacultyId { get; set; }
        public int NumberOfYears { get; set; }
        public int NumberOfSem { get; set; }
        public string? Objectives { get; set; }
        public string? Scopes { get; set; }
        public string? AdmissionRequirement { get; set; }
        public string? CourseOfStudy { get; set; }
        public string? ImageUrl { get; set; }
        public string? SyllabusUrl { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? Syllabus { get; set; }
        public List<SelectListItem>? FacultySelectList { get; set; }
    }
}