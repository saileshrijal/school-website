using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Models;
using SchoolWebsite.ViewModels;

namespace SchoolWebsite.Areas.UserManagement.Controllers;

[Area("UserManagement")]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly INotyfService _notyfService;

    public AccountController(SignInManager<ApplicationUser> signInManager,
                            INotyfService notyfService)
    {
        _signInManager = signInManager;
        _notyfService = notyfService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        try
        {
            if (ModelState.IsValid) { return View(vm); }
            var result = await _signInManager.PasswordSignInAsync(vm.UserName!, vm.Password!, vm.RememberMe, false);
            if (!result.Succeeded)
            {
                _notyfService.Error("Invalid username or password");
                return View(vm);
            }
            _notyfService.Success("Login successful");
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _notyfService.Error(ex.Message);
            return View(vm);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _notyfService.Success("Logout successful");
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _notyfService.Error(ex.Message);
            return RedirectToAction("Index", "Home");
        }
    }
}
