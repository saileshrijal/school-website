using SchoolWebsite.Models;

namespace SchoolWebsite.Repositories.Interface
{
    public interface IGalleryImageRepository : IRepository<GalleryImage>
    {
        Task<List<GalleryImage>> GetAllGalleryImagesWithGalleryAsync(string? search = null);
        Task<GalleryImage> GetGalleryImageWithGalleryByIdAsync(int id);
    }
}