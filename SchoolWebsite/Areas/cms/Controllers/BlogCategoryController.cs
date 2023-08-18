using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.BlogCategoryDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.BlogCategoryViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class BlogCategoryController : Controller
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly INotyfService _notyfService;

        public BlogCategoryController(IBlogCategoryRepository blogCategoryRepository,
                                    IBlogCategoryService blogCategoryService,
                                    INotyfService notyfService)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _blogCategoryService = blogCategoryService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var blogCategories = new List<BlogCategory>();
            if (search == null)
            {
                blogCategories = await _blogCategoryRepository.GetAllAsync();
            }
            else
            {
                blogCategories = await _blogCategoryRepository.FindByAsync(x => x.Name!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = blogCategories.Select(x => new BlogCategoryVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ParentCategoryName = x.ParentId == null ? "None" : x.Parent!.Name,
                Status = x.Status,
                CreatedDate = x.CreatedDate
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var blogCategories = await _blogCategoryRepository.GetAllAsync();
            var vm = new CreateBlogCategoryVm()
            {
                BlogCategorySelectList = blogCategories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogCategoryVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var blogCategories = await _blogCategoryRepository.GetAllAsync();
                    vm.BlogCategorySelectList = blogCategories.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreateBlogCategoryDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    ParentId = vm.ParentId,
                };
                await _blogCategoryService.CreateAsync(dto);
                _notyfService.Success("Blog category created successfully");
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
                var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
                var vm = new EditBlogCategoryVm()
                {
                    Id = blogCategory.Id,
                    Name = blogCategory.Name,
                    Description = blogCategory.Description,
                    ParentId = blogCategory.ParentId,
                };
                var blogCategories = await _blogCategoryRepository.GetAllAsync();
                vm.BlogCategorySelectList = blogCategories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Id == blogCategory.ParentId
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
        public async Task<IActionResult> Edit(EditBlogCategoryVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var blogCategories = await _blogCategoryRepository.GetAllAsync();
                    vm.BlogCategorySelectList = blogCategories.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = x.Id == vm.ParentId
                    }).ToList();
                    return View(vm);
                }
                var dto = new EditBlogCategoryDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description,
                    ParentId = vm.ParentId,
                };
                await _blogCategoryService.EditAsync(dto);
                _notyfService.Success("Blog category updated successfully");
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
                await _blogCategoryService.DeleteAsync(id);
                _notyfService.Success("Blog category deleted successfully");
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
                await _blogCategoryService.ToggleStatusAsync(id);
                _notyfService.Success("Blog category status changed successfully");
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