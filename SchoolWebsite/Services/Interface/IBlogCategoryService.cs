using SchoolWebsite.Dtos.BlogCategoryDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IBlogCategoryService
    {
        Task CreateAsync(CreateBlogCategoryDto createBlogCategoryDto);
        Task EditAsync(EditBlogCategoryDto editBlogCategoryDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}