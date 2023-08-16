using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}