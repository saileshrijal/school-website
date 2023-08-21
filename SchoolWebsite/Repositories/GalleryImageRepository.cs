using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class GalleryImageRepository : Repository<GalleryImage>, IGalleryImageRepository
    {
        public GalleryImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<GalleryImage>> GetAllGalleryImagesWithGalleryAsync(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
                return await _context.GalleryImages!.Include(g => g.Gallery).ToListAsync();
            else
                return await _context.GalleryImages!.Include(g => g.Gallery).Where(g => g.Gallery!.Name!.Contains(search)).ToListAsync();
        }

        public async Task<GalleryImage> GetGalleryImageWithGalleryByIdAsync(int id)
        {
            return await _context.GalleryImages!.Include(g => g.Gallery).FirstOrDefaultAsync(g => g.Id == id) ?? throw new Exception("Gallery Image not found");
        }
    }
}