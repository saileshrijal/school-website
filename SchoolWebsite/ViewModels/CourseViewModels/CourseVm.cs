namespace SchoolWebsite.ViewModels.CourseViewModels
{
    public class CourseVm
    {
        public int Id { get; set; }
         public string? Name { get; set; }
        public string? FacultyName { get; set; }
        public int NumberOfYears { get; set; }
        public int NumberOfSem { get; set; }
        public string? Objectives { get; set; }
        public string? Scopes { get; set; }
        public string? AdmissionRequirement { get; set; }
        public string? CourseOfStudy { get; set; }
        public string? ImageUrl { get; set; }
        public string? SyllabusUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}