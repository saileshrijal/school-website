
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.DesignationDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.DesignationViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;
        private readonly IDesignationRepository _designationRepository;
        private readonly INotyfService _notyfService;

        public DesignationController(IDesignationService designationService,
                                        IDesignationRepository designationRepository,
                                        INotyfService notyfService)
        {
            _designationService = designationService;
            _designationRepository = designationRepository;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            // implementing search functionality
            var designations = new List<Designation>();
            if(search == null)
            {
                designations = await _designationRepository.GetAllAsync();
            }
            else
            {
                designations = await _designationRepository.FindByAsync(x=>x.Name!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = designations.Select(x => new DesignationVm()
            {
                Id = x.Id,
                Name = x.Name,
                Position = x.Position,
                Status = x.Status,
                CreatedDate = x.CreatedDate
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesignationVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new CreateDesignationDto()
                {
                    Name = vm.Name,
                    Position = vm.Position
                };
                await _designationService.CreateAsync(dto);
                _notyfService.Success("Designation created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);

            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var designation = await _designationRepository.GetByIdAsync(id);
            var vm = new EditDesignationVm()
            {
                Id = designation.Id,
                Name = designation.Name,
                Position = designation.Position
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDesignationVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new EditDesignationDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Position = vm.Position
                };
                await _designationService.EditAsync(dto);
                _notyfService.Success("Designation updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _designationService.DeleteAsync(id);
                _notyfService.Success("Designation deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                await _designationService.ToggleStatusAsync(id);
                _notyfService.Success("Designation status changed successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}