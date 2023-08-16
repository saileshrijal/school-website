using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class GalleryRepository : Repository<Gallery>, IGalleryRepository
    {
        public GalleryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}