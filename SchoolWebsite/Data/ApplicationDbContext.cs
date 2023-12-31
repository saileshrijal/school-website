using SchoolWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Organization>? Organizations { get; set; }
        public DbSet<Designation>? Designations { get; set; }
        public DbSet<Faculty>? Faculties { get; set; }
        public DbSet<Gallery>? Galleries { get; set; }
        public DbSet<Link>? Links { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<PageCategory>? PageCategories { get; set; }
        public DbSet<BlogCategory>? BlogCategories { get; set; }
        public DbSet<Faq>? Faqs { get; set; }
        public DbSet<Testimonial>? Testimonials { get; set; }
        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<Notice>? Notices { get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<GalleryImage>? GalleryImages { get; set; }
        public DbSet<Page>? Pages { get; set; }
        public DbSet<Event>? Events { get; set; }
    }
}