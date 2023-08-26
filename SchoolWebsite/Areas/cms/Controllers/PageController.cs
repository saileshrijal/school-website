using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.PageDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.PageViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;
        private readonly IPageService _pageService;
        private readonly IPageCategoryRepository _pageCategoryRepository;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public PageController(IPageRepository pageRepository, IPageService pageService, IPageCategoryRepository pageCategoryRepository, INotyfService notyfService, IFileHelper fileHelper)
        {
            _pageRepository = pageRepository;
            _pageService = pageService;
            _pageCategoryRepository = pageCategoryRepository;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var pages = await _pageRepository.GetAllPagesWithCategoryAsync(search);
            ViewBag.Search = search;
            var vm = pages.Select(x => new PageVm()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.PageCategory!.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                HomePage = x.HomePage,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _pageCategoryRepository.GetAllAsync();
            var vm = new CreatePageVm()
            {
                CategorySelectList = categories.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _pageCategoryRepository.GetAllAsync();
                    vm.CategorySelectList = categories.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreatePageDto()
                {
                    Name = vm.Name,
                    PageCategoryId = vm.PageCategoryId,
                    Description = vm.Description,
                };

                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "pages");
                    dto.ImageUrl = imageUrl;
                }
                await _pageService.CreateAsync(dto);
                _notyfService.Success("Page created successfully");
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
                var page = await _pageRepository.GetPageWithCategoryByIdAsync(id);
                var categories = await _pageCategoryRepository.GetAllAsync();
                var vm = new EditPageVm()
                {
                    Id = page.Id,
                    Name = page.Name,
                    PageCategoryId = page.PageCategoryId,
                    Description = page.Description,
                    ImageUrl = page.ImageUrl,
                    CategorySelectList = categories.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
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
        public async Task<IActionResult> Edit(EditPageVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _pageCategoryRepository.GetAllAsync();
                    vm.CategorySelectList = categories.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var page = await _pageRepository.GetByIdAsync(vm.Id);
                var dto = new EditPageDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    PageCategoryId = vm.PageCategoryId,
                    Description = vm.Description,
                    ImageUrl = page.ImageUrl
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(page.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(page.ImageUrl, "pages");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "pages");
                    dto.ImageUrl = imageUrl;
                }
                await _pageService.EditAsync(dto);
                _notyfService.Success("Page updated successfully");
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
                var page = await _pageRepository.GetByIdAsync(id);
                if (page.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(page.ImageUrl, "pages");
                }
                await _pageService.DeleteAsync(id);
                _notyfService.Success("Page deleted successfully");
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
                await _pageService.ToggleStatusAsync(id);
                _notyfService.Success("Page status updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleHomePage(int id)
        {
            try
            {
                await _pageService.ToggleHomePageAsync(id);
                _notyfService.Success("Page's home page status updated successfully");
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