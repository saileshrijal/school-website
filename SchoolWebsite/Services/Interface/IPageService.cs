using SchoolWebsite.Dtos.PageDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IPageService
    {
        Task CreateAsync(CreatePageDto createPageDto);
        Task EditAsync(EditPageDto editPageDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
        Task ToggleHomePageAsync(int id);
    }
}