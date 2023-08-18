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
    }
}