using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWebsite.Models;

namespace SchoolWebsite.Repositories.Interface
{
    public interface ITestimonialRepository : IRepository<Testimonial>
    {
        Task<List<Testimonial>> GetAllTestimonialsWithDesignationAsync(string? search = null);
        Task<Testimonial> GetTestimonialWithDesignationAsync(int id);
    }
}