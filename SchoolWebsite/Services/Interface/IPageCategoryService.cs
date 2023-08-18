using SchoolWebsite.Dtos.PageCategoryDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IPageCategoryService
    {
        Task CreateAsync(CreatePageCategoryDto createPageCategoryDto);
        Task EditAsync(EditPageCategoryDto editPageCategoryDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}