using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        public DesignationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}