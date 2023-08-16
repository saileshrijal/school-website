using SchoolWebsite.Dtos.LinkDto;

namespace SchoolWebsite.Services.Interface
{
    public interface ILinkService
    {
        Task CreateAsync(CreateLinkDto createLinkDto);
        Task EditAsync(EditLinkDto editLinkDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}