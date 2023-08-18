using SchoolWebsite.Dtos.BlogDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IBlogService
    {
        Task CreateAsync(CreateBlogDto createBlogDto);
        Task EditAsync(EditBlogDto editBlogDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}