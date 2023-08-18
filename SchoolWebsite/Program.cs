using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using BookShop.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Data;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Seeder;
using SchoolWebsite.Seeder.Interface;
using SchoolWebsite.Services;
using SchoolWebsite.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); ;
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options=>{
    options.LoginPath = "/UserManagement/Account/Login";
});

builder.Services.AddNotyf(options =>
    {
        options.DurationInSeconds = 10;
        options.IsDismissable = true;
        options.Position = NotyfPosition.BottomRight;
    });


builder.Services.AddScoped<IUserSeeder, UserSeeder>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Helpers
builder.Services.AddScoped<IFileHelper, FileHelper>();

// Repositories
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IPageCategoryRepository, PageCategoryRepository>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();


// Services
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddScoped<IPageCategoryService, PageCategoryService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseNotyf();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area=Administrator}/{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

// app.MapControllerRoute(
// name: "default",
// pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
