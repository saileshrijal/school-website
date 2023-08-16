using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.GalleryDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.GalleryViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class GalleryController : Controller
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IGalleryService _galleryService;
        private readonly INotyfService _notyfService;

        public GalleryController(IGalleryRepository galleryRepository,
                                    IGalleryService galleryService,
                                    INotyfService notyfService)
        {
            _galleryRepository = galleryRepository;
            _galleryService = galleryService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var galleries = new List<Gallery>();
            if (search == null)
            {
                galleries = await _galleryRepository.GetAllAsync();
            }
            else
            {
                galleries = await _galleryRepository.FindByAsync(x => x.Name!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = galleries.Select(x => new GalleryVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
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
        public async Task<IActionResult> Create(CreateGalleryVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new CreateGalleryDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                };
                await _galleryService.CreateAsync(dto);
                _notyfService.Success("Gallery created successfully");
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
            var gallery = await _galleryRepository.GetByIdAsync(id);
            var vm = new EditGalleryVm()
            {
                Id = gallery.Id,
                Name = gallery.Name,
                Description = gallery.Description,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGalleryVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new EditGalleryDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description,
                };
                await _galleryService.EditAsync(dto);
                _notyfService.Success("Gallery updated successfully");
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
                await _galleryService.DeleteAsync(id);
                _notyfService.Success("Gallery deleted successfully");
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
                await _galleryService.ToggleStatusAsync(id);
                _notyfService.Success("Gallery status changed successfully");
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