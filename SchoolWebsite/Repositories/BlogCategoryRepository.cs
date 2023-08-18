using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class BlogCategoryRepository : Repository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}