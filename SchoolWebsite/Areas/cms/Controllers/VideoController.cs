
using System.Text.RegularExpressions;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.FacultyDto;
using SchoolWebsite.Dtos.VideoDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.DesignationViewModels;
using SchoolWebsite.ViewModels.FacultyViewModels;
using SchoolWebsite.ViewModels.VideoViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoService _videoService;
        private readonly INotyfService _notyfService;

        public VideoController(IVideoRepository videoRepository,
                                    IVideoService videoService,
                                    INotyfService notyfService)
        {
            _videoRepository = videoRepository;
            _videoService = videoService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            // implementing search functionality
            var videos = new List<Video>();
            if (search == null)
            {
                videos = await _videoRepository.GetAllAsync();
            }
            else
            {
                videos = await _videoRepository.FindByAsync(x => x.Name!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = videos.Select(x => new VideoVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                VideoUrl = x.VideoUrl,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVideoVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new CreateVideoDto()
                {
                    Name = vm.Name,
                    Description = vm.Description,
                };
                dto.VideoUrl = GetVideoId(vm.VideoUrl!);
                await _videoService.CreateAsync(dto);
                _notyfService.Success("Video created successfully");
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
            var video = await _videoRepository.GetByIdAsync(id);
            var vm = new EditVideoVm()
            {
                Id = video.Id,
                Name = video.Name,
                VideoUrl = video.VideoUrl,
                Description = video.Description,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVideoVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new EditVideoDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description,
                };
                dto.VideoUrl = GetVideoId(vm.VideoUrl!);
                await _videoService.EditAsync(dto);
                _notyfService.Success("Video updated successfully");
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
                await _videoService.DeleteAsync(id);
                _notyfService.Success("Video deleted successfully");
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
                await _videoService.ToggleStatusAsync(id);
                _notyfService.Success("Video status changed successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        private string GetVideoId(string url)
        {
            string pattern = @"(?:\/|v=|vi=|v%3D|v\/|vi\/|u\/\w\/|embed\/|e\/|user\/\w\/|watch\?v=|\&v=|\?v=)([a-zA-Z0-9_\-]{11})";
            Match match = Regex.Match(url, pattern);
            return match.Groups[1].Value;
        }
    }
}