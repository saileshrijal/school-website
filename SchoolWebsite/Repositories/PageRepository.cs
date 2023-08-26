using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Page>> GetAllPagesWithCategoryAsync(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                return await _context.Pages!.Include(p => p.PageCategory).ToListAsync();
            }
            return await _context.Pages!.Include(p => p.PageCategory).Where(p => p.Name!.Contains(search)).ToListAsync();
        }

        public async Task<Page> GetPageWithCategoryByIdAsync(int id)
        {
            return await _context.Pages!.Include(p => p.PageCategory).FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception("Page not found");
        }
    }
}