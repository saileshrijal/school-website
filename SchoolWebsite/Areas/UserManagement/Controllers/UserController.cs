using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.ViewModels.UserViewModels;

namespace SchoolWebsite.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<ApplicationUser> userManager,
                                            INotyfService notyfService,
                                            IFileHelper fileHelper,
                                            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
            _roleManager = roleManager;
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var vm = new CreateUserVm()
            {
                RoleSelectList = roles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var checkUserName = await _userManager.FindByNameAsync(vm.UserName!);
                if (checkUserName != null)
                {
                    _notyfService.Error("Username already exists. Please choose another username.");
                    return View(vm);
                }
                var checkEmail = await _userManager.FindByEmailAsync(vm.Email!);
                if (checkEmail != null)
                {
                    _notyfService.Error("Email already exists. Please choose another email.");
                    return View(vm);
                }
                var user = new ApplicationUser()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserName = vm.UserName,
                    Email = vm.Email,
                    TemporaryAddress = vm.TemporaryAddress,
                    PermanentAddress = vm.PermanentAddress,
                    PrimaryContact = vm.PrimaryContact,
                    SecondaryContact = vm.SecondaryContact,
                    CreatedDate = DateTime.UtcNow,
                    Status = true,
                };
                if (vm.Image != null)
                {
                    user.ImageUrl = await _fileHelper.UploadFileAsync(vm.Image, "User");
                }
                var result = await _userManager.CreateAsync(user, vm.Password!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return View(vm);
                }
                var role = await _roleManager.FindByIdAsync(vm.RoleId!);
                if (role == null)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return View(vm);
                }
                await _userManager.AddToRoleAsync(user, role.Name!);
                _notyfService.Success("User created successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found.");
                return RedirectToAction(nameof(Index));
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var vm = new EditUserVm()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TemporaryAddress = user.TemporaryAddress,
                PermanentAddress = user.PermanentAddress,
                PrimaryContact = user.PrimaryContact,
                SecondaryContact = user.SecondaryContact,
                ImageUrl = user.ImageUrl,
                RoleSelectList = roles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id,
                    Selected = _userManager.IsInRoleAsync(user, x.Name!).Result
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var user = await _userManager.FindByIdAsync(vm.Id!);
                if (user == null)
                {
                    _notyfService.Error("User not found.");
                    return RedirectToAction(nameof(Index));
                }
                var checkEmail = await _userManager.FindByEmailAsync(vm.Email!);
                if (checkEmail != null && checkEmail.Id != vm.Id)
                {
                    _notyfService.Error("Email already exists. Please choose another email.");
                    return View(vm);
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.TemporaryAddress = vm.TemporaryAddress;
                user.PermanentAddress = vm.PermanentAddress;
                user.PrimaryContact = vm.PrimaryContact;
                user.SecondaryContact = vm.SecondaryContact;
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(user.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(user.ImageUrl, "User");
                    }
                    user.ImageUrl = await _fileHelper.UploadFileAsync(vm.Image, "User");
                }
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return View(vm);
                }
                var roles = await _roleManager.Roles.ToListAsync();
                foreach (var role in roles)
                {
                    if (vm.RoleId == role.Id)
                    {
                        if (!_userManager.IsInRoleAsync(user, role.Name!).Result)
                        {
                            await _userManager.AddToRoleAsync(user, role.Name!);
                        }
                    }
                    else
                    {
                        if (_userManager.IsInRoleAsync(user, role.Name!).Result)
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name!);
                        }
                    }
                }
                _notyfService.Success("User updated successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyfService.Error("User not found.");
                return RedirectToAction(nameof(Index));
            }
            var vm = new ResetPasswordVm()
            {
                UserId = user.Id,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var user = await _userManager.FindByIdAsync(vm.UserId!);
                if (user == null)
                {
                    _notyfService.Error("User not found.");
                    return RedirectToAction(nameof(Index));
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, vm.NewPassword!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return View(vm);
                }
                _notyfService.Success("Password reset successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        // delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found.");
                    return RedirectToAction(nameof(Index));
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return RedirectToAction(nameof(Index));
                }
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    await _fileHelper.DeleteFileAsync(user.ImageUrl, "User");
                }
                _notyfService.Success("User deleted successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // toggle status
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _notyfService.Error("User not found.");
                    return RedirectToAction(nameof(Index));
                }
                user.Status = !user.Status;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return RedirectToAction(nameof(Index));
                }
                _notyfService.Success("User status updated successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // profile
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null)
            {
                _notyfService.Error("User not found.");
                return RedirectToAction(nameof(Index));
            }
            var vm = new ProfileVm()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TemporaryAddress = user.TemporaryAddress,
                PermanentAddress = user.PermanentAddress,
                PrimaryContact = user.PrimaryContact,
                SecondaryContact = user.SecondaryContact,
                ImageUrl = user.ImageUrl,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
                if (user == null)
                {
                    _notyfService.Error("User not found.");
                    return RedirectToAction(nameof(Index));
                }
                var checkEmail = await _userManager.FindByEmailAsync(vm.Email!);
                if (checkEmail != null && checkEmail.Id != user.Id)
                {
                    _notyfService.Error("Email already exists. Please choose another email.");
                    return View(vm);
                }
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.TemporaryAddress = vm.TemporaryAddress;
                user.PermanentAddress = vm.PermanentAddress;
                user.PrimaryContact = vm.PrimaryContact;
                user.SecondaryContact = vm.SecondaryContact;
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(user.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(user.ImageUrl, "User");
                    }
                    user.ImageUrl = await _fileHelper.UploadFileAsync(vm.Image, "User");
                }
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return View(vm);
                }
                _notyfService.Success("Profile updated successfully.");
                return RedirectToAction(nameof(Profile));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var vm = new ChangePasswordVm();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
                if (user == null)
                {
                    _notyfService.Error("User not found.");
                    return RedirectToAction(nameof(Index));
                }
                var checkPassword = await _userManager.CheckPasswordAsync(user, vm.CurrentPassword!);
                if (!checkPassword)
                {
                    _notyfService.Error("Current password is incorrect.");
                    return View(vm);
                }
                var result = await _userManager.ChangePasswordAsync(user, vm.CurrentPassword!, vm.NewPassword!);
                if (!result.Succeeded)
                {
                    _notyfService.Error("Something went wrong. Please try again.");
                    return View(vm);
                }
                _notyfService.Success("Password changed successfully.");
                return RedirectToAction(nameof(Profile));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }
    }
}