using SchoolWebsite.Dtos.DesignationDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IDesignationService
    {
        Task CreateAsync(CreateDesignationDto createDesignationDto);
        Task EditAsync(EditDesignationDto editDesignationDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}