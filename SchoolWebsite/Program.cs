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
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
