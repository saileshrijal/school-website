using SchoolWebsite.Dtos.VideoDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IVideoService
    {
        Task CreateAsync(CreateVideoDto createVideoDto);
        Task EditAsync(EditVideoDto editVideoDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}