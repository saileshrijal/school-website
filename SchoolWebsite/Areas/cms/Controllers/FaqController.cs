using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Dtos.FaqDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.FaqViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class FaqController : Controller
    {
        private readonly IFaqRepository _faqRepository;
        private readonly IFaqService _faqService;
        private readonly INotyfService _notyfService;

        public FaqController(IFaqRepository faqRepository,
                                    IFaqService faqService,
                                    INotyfService notyfService)
        {
            _faqRepository = faqRepository;
            _faqService = faqService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var faqs = new List<Faq>();
            if (search == null)
            {
                faqs = await _faqRepository.GetAllAsync();
            }
            else
            {
                faqs = await _faqRepository.FindByAsync(x => x.Question!.Contains(search));
            }
            ViewBag.Search = search;
            var vm = faqs.Select(x => new FaqVm()
            {
                Id = x.Id,
                Question = x.Question,
                Answer = x.Answer,
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
        public async Task<IActionResult> Create(CreateFaqVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new CreateFaqDto()
                {
                    Question = vm.Question,
                    Answer = vm.Answer,
                };
                await _faqService.CreateAsync(dto);
                _notyfService.Success("FAQ created successfully");
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
            var faq = await _faqRepository.GetByIdAsync(id);
            var vm = new EditFaqVm()
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFaqVm vm)
        {
            try
            {
                if (!ModelState.IsValid) { return View(vm); }
                var dto = new EditFaqDto()
                {
                    Id = vm.Id,
                    Question = vm.Question,
                    Answer = vm.Answer,
                };
                await _faqService.EditAsync(dto);
                _notyfService.Success("FAQ updated successfully");
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
                await _faqService.DeleteAsync(id);
                _notyfService.Success("FAQ deleted successfully");
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