using SchoolWebsite.Dtos.GalleryDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IGalleryService
    {
        Task CreateAsync(CreateGalleryDto createGalleryDto);
        Task EditAsync(EditGalleryDto editGalleryDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}