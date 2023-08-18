using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.PageCategoryDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.PageCategoryViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class PageCategoryController : Controller
    {
        private readonly IPageCategoryRepository _pageCategoryRepository;
        private readonly IPageCategoryService _pageCategoryService;
        private readonly INotyfService _notyfService;

        public PageCategoryController(IPageCategoryRepository pageCategoryRepository,
                                    IPageCategoryService pageCategoryService,
                                    INotyfService notyfService)
        {
            _pageCategoryRepository = pageCategoryRepository;
            _pageCategoryService = pageCategoryService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var pageCategories = new List<PageCategory>();
            if (search == null)
            {
                pageCategories = await _pageCategoryRepository.GetAllAsync();
            }
            else
            {
                pageCategories = await _pageCategoryRepository.FindByAsync(x => x.Name!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = pageCategories.Select(x => new PageCategoryVm()
            {
                Id = x.Id,
                Name = x.Name,
                ParentCategoryName = x.ParentId == null ? "None" : x.Parent!.Name,
                Status = x.Status,
                CreatedDate = x.CreatedDate
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var pageCategories = await _pageCategoryRepository.GetAllAsync();
            var vm = new CreatePageCategoryVm()
            {
                PageCategorySelectList = pageCategories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePageCategoryVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var pageCategories = await _pageCategoryRepository.GetAllAsync();
                    vm.PageCategorySelectList = pageCategories.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreatePageCategoryDto()
                {
                    Name = vm.Name,
                    ParentId = vm.ParentId,
                };
                await _pageCategoryService.CreateAsync(dto);
                _notyfService.Success("Page category created successfully");
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
            try
            {
                var pageCategory = await _pageCategoryRepository.GetByIdAsync(id);
                var vm = new EditPageCategoryVm()
                {
                    Id = pageCategory.Id,
                    Name = pageCategory.Name,
                    ParentId = pageCategory.ParentId,
                };
                var pageCategories = await _pageCategoryRepository.GetAllAsync();
                vm.PageCategorySelectList = pageCategories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == pageCategory.ParentId
                }).ToList();
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPageCategoryVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var pageCategories = await _pageCategoryRepository.GetAllAsync();
                    vm.PageCategorySelectList = pageCategories.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = x.Id == vm.ParentId
                    }).ToList();
                    return View(vm);
                }
                var dto = new EditPageCategoryDto()
                {
                    Id = vm.Id,
                    ParentId = vm.ParentId,
                    Name = vm.Name,
                };
                await _pageCategoryService.EditAsync(dto);
                _notyfService.Success("Page category updated successfully");
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
                await _pageCategoryService.DeleteAsync(id);
                _notyfService.Success("Page category deleted successfully");
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
                await _pageCategoryService.ToggleStatusAsync(id);
                _notyfService.Success("Page category status changed successfully");
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