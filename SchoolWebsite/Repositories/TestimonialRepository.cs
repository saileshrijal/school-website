using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Data;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;

namespace SchoolWebsite.Repositories
{
    public class TestimonialRepository : Repository<Testimonial>, ITestimonialRepository
    {
        public TestimonialRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Testimonial>> GetAllTestimonialsWithDesignationAsync(string? search = null)
        {
            if (string.IsNullOrEmpty(search))
                return await _context.Testimonials!.Include(t => t.Designation).ToListAsync();
            else
                return await _context.Testimonials!.Include(t => t.Designation).Where(t => t.Name!.Contains(search)).ToListAsync();
        }

        public async Task<Testimonial> GetTestimonialWithDesignationAsync(int id)
        {
            return await _context.Testimonials!.Include(t => t.Designation).FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception("Testimonial not found");
        }
    }
}