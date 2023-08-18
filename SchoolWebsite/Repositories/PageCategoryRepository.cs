using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class PageCategoryRepository : Repository<PageCategory>, IPageCategoryRepository
    {
        public PageCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}