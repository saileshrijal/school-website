using SchoolWebsite.Dtos.GalleryImageDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IGalleryImageService
    {
        Task CreateAsync(CreateGalleryImageDto createGalleryImageDto);
        Task EditAsync(EditGalleryImageDto editGalleryImageDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
        Task ToggleSliderStatusAsync(int id);
    }
}