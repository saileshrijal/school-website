using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels;

namespace SchoolWebsite.Areas.UserManagement.Controllers
{
    [Area("UserManagement")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationService _organizationService;
        private readonly IFileHelper _fileHelper;
        private readonly INotyfService _notyfService;

        public OrganizationController(IOrganizationRepository organizationRepository,
                                        IOrganizationService organizationService,
                                        IFileHelper fileHelper,
                                        INotyfService notyfService)
        {
            _organizationRepository = organizationRepository;
            _organizationService = organizationService;
            _fileHelper = fileHelper;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var organizations = await _organizationRepository.GetAllAsync();
            if (organizations.Count == 0)
            {
                return View(new OrganizationVm());
            }
            var organization = organizations.FirstOrDefault();
            var vm = new OrganizationVm
            {
                Id = organization!.Id,
                Name = organization.Name,
                ShortName = organization.ShortName,
                Address = organization.Address,
                Phone = organization.Phone,
                Email = organization.Email,
                ContactPerson = organization.ContactPerson,
                PanNumber = organization.PanNumber,
                FacebookUrl = organization.FacebookUrl,
                TwitterUrl = organization.TwitterUrl,
                InstagramUrl = organization.InstagramUrl,
                YoutubeUrl = organization.YoutubeUrl,
                Description = organization.Description,
                LogoUrl = organization.LogoUrl,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(OrganizationVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                if (vm.Id == 0)
                {
                    //create
                    var dto = new OrganizationDto
                    {
                        Name = vm.Name,
                        ShortName = vm.ShortName,
                        Address = vm.Address,
                        Phone = vm.Phone,
                        Email = vm.Email,
                        ContactPerson = vm.ContactPerson,
                        PanNumber = vm.PanNumber,
                        FacebookUrl = vm.FacebookUrl,
                        TwitterUrl = vm.TwitterUrl,
                        InstagramUrl = vm.InstagramUrl,
                        YoutubeUrl = vm.YoutubeUrl,
                        Description = vm.Description,
                    };
                    if (vm.Logo != null)
                    {
                        dto.LogoUrl = await _fileHelper.UploadFileAsync(vm.Logo, "organization");
                    }
                    await _organizationService.CreateAsync(dto);
                    _notyfService.Success("Organization profile created successfully.");
                }
                else
                {
                    var organizations = await _organizationRepository.GetAllAsync();
                    var organization = organizations.FirstOrDefault();
                    // update
                    var dto = new OrganizationDto
                    {
                        Id = vm.Id,
                        Name = vm.Name,
                        ShortName = vm.ShortName,
                        Address = vm.Address,
                        Phone = vm.Phone,
                        Email = vm.Email,
                        ContactPerson = vm.ContactPerson,
                        PanNumber = vm.PanNumber,
                        FacebookUrl = vm.FacebookUrl,
                        TwitterUrl = vm.TwitterUrl,
                        InstagramUrl = vm.InstagramUrl,
                        YoutubeUrl = vm.YoutubeUrl,
                        Description = vm.Description,
                    };
                    if (vm.Logo != null)
                    {
                        if (!string.IsNullOrEmpty(organization!.LogoUrl))
                        {
                            await _fileHelper.DeleteFileAsync(organization.LogoUrl, "organization");
                        }
                        dto.LogoUrl = await _fileHelper.UploadFileAsync(vm.Logo, "organization");
                    }
                    await _organizationService.EditAsync(dto);
                    _notyfService.Success("Organization profile updated successfully.");
                }
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