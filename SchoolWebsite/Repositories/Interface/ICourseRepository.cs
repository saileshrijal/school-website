using SchoolWebsite.Models;

namespace SchoolWebsite.Repositories.Interface
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<Course>> GetAllCoursesWithFacultyAsync(string? search = null);
        Task<Course> GetCourseWithFacultyAsync(int id);
    }
}