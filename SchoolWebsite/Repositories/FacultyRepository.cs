using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}