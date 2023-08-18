using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Course>> GetAllCoursesWithFacultyAsync(string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.Courses!.Include(c => c.Faculty).Where(c => c.Name!.Contains(search)).ToListAsync();
            }
            else
            {
                return await _context.Courses!.Include(c => c.Faculty).ToListAsync();
            }
        }

        public async Task<Course> GetCourseWithFacultyAsync(int id)
        {
            return await _context.Courses!.Include(c => c.Faculty).FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Course not found");
        }
    }
}