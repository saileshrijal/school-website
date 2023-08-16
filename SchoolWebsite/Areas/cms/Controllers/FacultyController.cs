
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.FacultyDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.DesignationViewModels;
using SchoolWebsite.ViewModels.FacultyViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class FacultyController : Controller
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IFacultyService _facultyService;
        private readonly INotyfService _notyfService;

        public FacultyController(IFacultyRepository facultyRepository,
                                    IFacultyService facultyService,
                                    INotyfService notyfService)
        {
            _facultyRepository = facultyRepository;
            _facultyService = facultyService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            // implementing search functionality
            var faculties = new List<Faculty>();
            if (search == null)
            {
                faculties = await _facultyRepository.GetAllAsync();
            }
            else
            {
                faculties = await _facultyRepository.FindByAsync(x => x.Name!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = faculties.Select(x => new FacultyVm()
            {
                Id = x.Id,
                Name = x.Name,
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
                var dto = new CreateFacultyDto()
                {
                    Name = vm.Name,
                };
                await _facultyService.CreateAsync(dto);
                _notyfService.Success("Faculty created successfully");
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
            var faculty = await _facultyRepository.GetByIdAsync(id);
            var vm = new EditFacultyVm()
            {
                Id = faculty.Id,
                Name = faculty.Name,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFacultyVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new EditFacultyDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                };
                await _facultyService.EditAsync(dto);
                _notyfService.Success("Faculty updated successfully");
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
                await _facultyService.DeleteAsync(id);
                _notyfService.Success("Faculty deleted successfully");
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
                await _facultyService.ToggleStatusAsync(id);
                _notyfService.Success("Faculty status changed successfully");
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