using SchoolWebsite.Models;

namespace SchoolWebsite.Repositories.Interface
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<Course>> GetAllCoursesWithFaculty(string? search = null);
        Task<Course> GetCourseWithFaculty(int id);
    }
}