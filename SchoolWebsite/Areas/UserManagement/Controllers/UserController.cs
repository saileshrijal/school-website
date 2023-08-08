using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Models;
using SchoolWebsite.ViewModels.UserViewModels;

namespace SchoolWebsite.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        public UserController(UserManager<ApplicationUser> userManager,
                                            INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var vm = users.Select(x => new UserVm()
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Email = x.Email,
                FullName = x.FullName,
                UserName = x.UserName,
                TemporaryAddress = x.TemporaryAddress,
                PrimaryContact = x.PrimaryContact,
                CreatedDate = x.CreatedDate,
                Status = x.Status,
                Role = _userManager.GetRolesAsync(x).Result[0]

            }).ToList();
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}