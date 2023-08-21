using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.GalleryImageDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.GalleryImageViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class ImageController : Controller
    {
        private readonly IGalleryImageRepository _galleryImageRepository;
        private readonly IGalleryImageService _galleryImageService;
        private readonly IGalleryRepository _galleryRepository;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public ImageController(IGalleryImageRepository galleryImageRepository, IGalleryImageService galleryImageService, IGalleryRepository galleryRepository, INotyfService notyfService, IFileHelper fileHelper)
        {
            _galleryImageRepository = galleryImageRepository;
            _galleryImageService = galleryImageService;
            _galleryRepository = galleryRepository;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var galleryImages = await _galleryImageRepository.GetAllGalleryImagesWithGalleryAsync(search);
            ViewBag.Search = search;
            var vm = galleryImages.Select(x => new GalleryImageVm()
            {
                Id = x.Id,
                GalleryName = x.Gallery!.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Status = x.Status,
                Slider = x.Slider,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var galleries = await _galleryRepository.GetAllAsync();
            var vm = new CreateGalleryImageVm()
            {
                GallerySelectList = galleries.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGalleryImageVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var galleries = await _galleryRepository.GetAllAsync();
                    vm.GallerySelectList = galleries.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreateGalleryImageDto()
                {
                    GalleryId = vm.GalleryId,
                    Description = vm.Description,
                };
                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "galleries");
                    dto.ImageUrl = imageUrl;
                }
                await _galleryImageService.CreateAsync(dto);
                _notyfService.Success("Gallery image created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var galleryImage = await _galleryImageRepository.GetGalleryImageWithGalleryByIdAsync(id);
                var galleries = await _galleryRepository.GetAllAsync();
                var vm = new EditGalleryImageVm()
                {
                    Id = galleryImage.Id,
                    GalleryId = galleryImage.GalleryId,
                    Description = galleryImage.Description,
                    ImageUrl = galleryImage.ImageUrl,
                    GallerySelectList = galleries.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                        Selected = x.Id == galleryImage.GalleryId
                    }).ToList()
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGalleryImageVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var galleries = await _galleryRepository.GetAllAsync();
                    vm.GallerySelectList = galleries.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var galleryImage = await _galleryImageRepository.GetByIdAsync(vm.Id);
                var dto = new EditGalleryImageDto()
                {
                    Id = vm.Id,
                    GalleryId = vm.GalleryId,
                    Description = vm.Description,
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(galleryImage.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(galleryImage.ImageUrl, "galleries");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "galleries");
                    dto.ImageUrl = imageUrl;
                }
                await _galleryImageService.EditAsync(dto);
                _notyfService.Success("Gallery image updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var galleryImage = await _galleryImageRepository.GetByIdAsync(id);
                if (galleryImage.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(galleryImage.ImageUrl, "galleries");
                }
                await _galleryImageService.DeleteAsync(id);
                _notyfService.Success("Gallery image deleted successfully");
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
                await _galleryImageService.ToggleStatusAsync(id);
                _notyfService.Success("Gallery image status updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleSliderStatus(int id)
        {
            try
            {
                await _galleryImageService.ToggleSliderStatusAsync(id);
                _notyfService.Success("Gallery image slider status updated successfully");
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