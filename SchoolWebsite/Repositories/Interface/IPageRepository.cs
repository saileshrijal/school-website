using SchoolWebsite.Models;

namespace SchoolWebsite.Repositories.Interface
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<List<Page>> GetAllPagesWithCategoryAsync(string? search = null);
        Task<Page> GetPageWithCategoryByIdAsync(int id);
    }
}