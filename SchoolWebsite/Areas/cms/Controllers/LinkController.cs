using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.LinkDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

using SchoolWebsite.ViewModels.LinkViewModel;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class LinkController : Controller
    {
        private readonly ILinkRepository _linkRepository;
        private readonly ILinkService _linkService;
        private readonly INotyfService _notyfService;

        public LinkController(ILinkRepository linkRepository,
                                    ILinkService linkService,
                                    INotyfService notyfService)
        {
            _linkRepository = linkRepository;
            _linkService = linkService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var links = new List<Link>();
            if (search == null)
            {
                links = await _linkRepository.GetAllAsync();
            }
            else
            {
                links = await _linkRepository.FindByAsync(x => x.WebsiteUrl!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = links.Select(x => new LinkVm()
            {
                Id = x.Id,
                WebsiteUrl = x.WebsiteUrl,
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
        public async Task<IActionResult> Create(CreateLinkVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new CreateLinkDto()
                {
                    WebsiteUrl = vm.WebsiteUrl,
                };
                await _linkService.CreateAsync(dto);
                _notyfService.Success("Link created successfully");
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
            var link = await _linkRepository.GetByIdAsync(id);
            var vm = new EditLinkVm()
            {
                Id = link.Id,
                WebsiteUrl = link.WebsiteUrl,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLinkVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new EditLinkDto()
                {
                    Id = vm.Id,
                    WebsiteUrl = vm.WebsiteUrl,
                };
                await _linkService.EditAsync(dto);
                _notyfService.Success("Link updated successfully");
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
                await _linkService.DeleteAsync(id);
                _notyfService.Success("Link deleted successfully");
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
                await _linkService.ToggleStatusAsync(id);
                _notyfService.Success("Link status changed successfully");
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