using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Blog>> GetAllBlogsWithCategoryAsync(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _context.Blogs!.Include(x => x.BlogCategory).ToListAsync();
            }
            return _context.Blogs!.Include(x => x.BlogCategory).Where(x => x.Title!.Contains(search)).ToListAsync();
        }

        public async Task<Blog> GetBlogWithCategoryByIdAsync(int id)
        {
            return await _context.Blogs!.Include(x => x.BlogCategory).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Blog not found");
        }
    }
}