using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.NoticeDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.NoticeViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class NoticeController : Controller
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly INoticeService _noticeService;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public NoticeController(INoticeRepository noticeRepository, INoticeService noticeService, INotyfService notyfService, IFileHelper fileHelper)
        {
            _noticeRepository = noticeRepository;
            _noticeService = noticeService;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var notices = new List<Notice>();
            if (string.IsNullOrEmpty(search))
                notices = await _noticeRepository.GetAllAsync();
            else
                notices = await _noticeRepository.FindByAsync(x => x.Title!.Contains(search));
            ViewBag.Search = search;
            var vm = notices.Select(x => new NoticeVm()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PopUp = x.PopUp,
                ExpiryDate = x.ExpiryDate,
                ImageUrl = x.ImageUrl,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoticeVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var dto = new CreateNoticeDto()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    PopUp = vm.PopUp,
                    ExpiryDate = vm.ExpiryDate,
                };
                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "notices");
                    dto.ImageUrl = imageUrl;
                }
                await _noticeService.CreateAsync(dto);
                _notyfService.Success("Notice created successfully");
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
                var notice = await _noticeRepository.GetByIdAsync(id);
                var vm = new EditNoticeVm()
                {
                    Id = notice.Id,
                    Title = notice.Title,
                    Description = notice.Description,
                    PopUp = notice.PopUp,
                    ExpiryDate = notice.ExpiryDate,
                    ImageUrl = notice.ImageUrl,
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
        public async Task<IActionResult> Edit(EditNoticeVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var notice = await _noticeRepository.GetByIdAsync(vm.Id);
                var dto = new EditNoticeDto()
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Description = vm.Description,
                    PopUp = vm.PopUp,
                    ExpiryDate = vm.ExpiryDate,
                    ImageUrl = notice.ImageUrl,
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(notice.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(notice.ImageUrl, "notices");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "notices");
                    dto.ImageUrl = imageUrl;
                }
                await _noticeService.EditAsync(dto);
                _notyfService.Success("Notice updated successfully");
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
                var notice = await _noticeRepository.GetByIdAsync(id);
                if (notice.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(notice.ImageUrl, "notices");
                }
                await _noticeService.DeleteAsync(id);
                _notyfService.Success("Notice deleted successfully");
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
                await _noticeService.ToggleStatusAsync(id);
                _notyfService.Success("Notice status updated successfully");
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