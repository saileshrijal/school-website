using System.Net.Mime;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.EventDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.EventViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventService _eventService;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;

        public EventController(IEventRepository eventRepository, IEventService eventService, INotyfService notyfService, IFileHelper fileHelper)
        {
            _eventRepository = eventRepository;
            _eventService = eventService;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var events = new List<Event>();
            if (string.IsNullOrEmpty(search))
                events = await _eventRepository.GetAllAsync();
            else
                events = await _eventRepository.FindByAsync(x => x.Title!.Contains(search));
            ViewBag.Search = search;
            var vm = events.Select(x => new EventVm()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Location = x.Location,
                Organizer = x.Organizer,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
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
        public async Task<IActionResult> Create(CreateEventVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var dto = new CreateEventDto()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    Location = vm.Location,
                    Organizer = vm.Organizer,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                };
                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "events");
                    dto.ImageUrl = imageUrl;
                }
                await _eventService.CreateAsync(dto);
                _notyfService.Success("Event created successfully");
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
                var eventObj = await _eventRepository.GetByIdAsync(id);
                var vm = new EditEventVm()
                {
                    Id = eventObj.Id,
                    Title = eventObj.Title,
                    Description = eventObj.Description,
                    Location = eventObj.Location,
                    Organizer = eventObj.Organizer,
                    StartDate = eventObj.StartDate,
                    EndDate = eventObj.EndDate,
                    ImageUrl = eventObj.ImageUrl,
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
        public async Task<IActionResult> Edit(EditEventVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                var eventObj = await _eventRepository.GetByIdAsync(vm.Id);
                var dto = new EditEventDto()
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Description = vm.Description,
                    Location = vm.Location,
                    Organizer = vm.Organizer,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    ImageUrl = eventObj.ImageUrl,
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(eventObj.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(eventObj.ImageUrl, "events");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "events");
                    dto.ImageUrl = imageUrl;
                }
                await _eventService.EditAsync(dto);
                _notyfService.Success("Event updated successfully");
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
                var eventObj = await _eventRepository.GetByIdAsync(id);
                if (eventObj.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(eventObj.ImageUrl, "events");
                }
                await _eventService.DeleteAsync(id);
                _notyfService.Success("Event deleted successfully");
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
                await _eventService.ToggleStatusAsync(id);
                _notyfService.Success("Event status updated successfully");
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