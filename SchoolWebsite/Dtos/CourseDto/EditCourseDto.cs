namespace SchoolWebsite.Dtos.CourseDto
{
    public class EditCourseDto
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
    }
}