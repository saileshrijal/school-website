using SchoolWebsite.Models;

namespace SchoolWebsite.Repositories.Interface
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<List<Blog>> GetAllBlogsWithCategoryAsync(string? search = null);
        Task<Blog> GetBlogWithCategoryByIdAsync(int id);
    }
}