using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.BlogDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.BlogViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public BlogController(IBlogRepository blogRepository, IBlogService blogService, IBlogCategoryRepository blogCategoryRepository, INotyfService notyfService, IFileHelper fileHelper)
        {
            _blogRepository = blogRepository;
            _blogService = blogService;
            _blogCategoryRepository = blogCategoryRepository;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var blogs = await _blogRepository.GetAllBlogsWithCategoryAsync(search);
            ViewBag.Search = search;
            var vm = blogs.Select(x => new BlogVm()
            {
                Id = x.Id,
                Title = x.Title,
                CategoryName = x.BlogCategory!.Name,
                Description = x.Description,
                AuthorName = x.AuthorName,
                VideoUrl = x.VideoUrl,
                ImageUrl = x.ImageUrl,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _blogCategoryRepository.GetAllAsync();
            var vm = new CreateBlogVm()
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
        public async Task<IActionResult> Create(CreateBlogVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _blogCategoryRepository.GetAllAsync();
                    vm.CategorySelectList = categories.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreateBlogDto()
                {
                    Title = vm.Title,
                    BlogCategoryId = vm.BlogCategoryId,
                    Description = vm.Description,
                    VideoUrl = vm.VideoUrl,
                    AuthorName = vm.AuthorName
                };
                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "blogs");
                    dto.ImageUrl = imageUrl;
                }
                await _blogService.CreateAsync(dto);
                _notyfService.Success("Blog created successfully");
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
                var blog = await _blogRepository.GetBlogWithCategoryByIdAsync(id);
                var categories = await _blogCategoryRepository.GetAllAsync();
                var vm = new EditBlogVm()
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    BlogCategoryId = blog.BlogCategoryId,
                    Description = blog.Description,
                    ImageUrl = blog.ImageUrl,
                    VideoUrl = blog.VideoUrl,
                    AuthorName = blog.AuthorName,
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
        public async Task<IActionResult> Edit(EditBlogVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categories = await _blogCategoryRepository.GetAllAsync();
                    vm.CategorySelectList = categories.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var blog = await _blogRepository.GetByIdAsync(vm.Id);
                var dto = new EditBlogDto()
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    BlogCategoryId = vm.BlogCategoryId,
                    Description = vm.Description,
                    ImageUrl = blog.ImageUrl,
                    VideoUrl = vm.VideoUrl,
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(blog.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(blog.ImageUrl, "blogs");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "blogs");
                    dto.ImageUrl = imageUrl;
                }
                await _blogService.EditAsync(dto);
                _notyfService.Success("Blog updated successfully");
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
                var blog = await _blogRepository.GetByIdAsync(id);
                if (blog.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(blog.ImageUrl, "blogs");
                }
                await _blogService.DeleteAsync(id);
                _notyfService.Success("Blog deleted successfully");
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
                await _blogService.ToggleStatusAsync(id);
                _notyfService.Success("Blog status updated successfully");
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