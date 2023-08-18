using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.TestimonialDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.TestimonialViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly ITestimonialService _testimonialService;
        private readonly IDesignationRepository _designationRepository;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;
        public TestimonialController(ITestimonialRepository testimonialRepository, ITestimonialService testimonialService, IDesignationRepository designationRepository, INotyfService notyfService, IFileHelper fileHelper)
        {
            _testimonialRepository = testimonialRepository;
            _testimonialService = testimonialService;
            _designationRepository = designationRepository;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var testimonials = new List<Testimonial>();
            if (string.IsNullOrEmpty(search))
            {
                testimonials = await _testimonialRepository.GetAllTestimonialsWithDesignationAsync();
            }
            else
            {
                testimonials = await _testimonialRepository.GetAllTestimonialsWithDesignationAsync(search);
            }
            ViewBag.Search = search;
            var vm = testimonials.Select(x => new TestimonialVm()
            {
                Id = x.Id,
                Name = x.Name,
                DesignationName = x.Designation!.Name,
                Organization = x.Organization,
                Statement = x.Statement,
                ImageUrl = x.ImageUrl,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var designations = await _designationRepository.GetAllAsync();
            var vm = new CreateTestimonialVm()
            {
                DesignationSelectList = designations.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var designations = await _designationRepository.GetAllAsync();
                    vm.DesignationSelectList = designations.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreateTestimonialDto()
                {
                    Name = vm.Name,
                    DesignationId = vm.DesignationId,
                    Organization = vm.Organization,
                    Statement = vm.Statement,
                };
                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "testimonials");
                    dto.ImageUrl = imageUrl;
                }
                await _testimonialService.CreateAsync(dto);
                _notyfService.Success("Testimonial created successfully");
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
                var testimonial = await _testimonialRepository.GetTestimonialWithDesignationAsync(id);
                var designations = await _designationRepository.GetAllAsync();
                var vm = new EditTestimonialVm()
                {
                    Id = testimonial.Id,
                    Name = testimonial.Name,
                    DesignationId = testimonial.DesignationId,
                    Organization = testimonial.Organization,
                    Statement = testimonial.Statement,
                    ImageUrl = testimonial.ImageUrl,
                    DesignationSelectList = designations.Select(x => new SelectListItem()
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
        public async Task<IActionResult> Edit(EditTestimonialVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var designations = await _designationRepository.GetAllAsync();
                    vm.DesignationSelectList = designations.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var testimonial = await _testimonialRepository.GetByIdAsync(vm.Id);
                var dto = new EditTestimonialDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    DesignationId = vm.DesignationId,
                    Organization = vm.Organization,
                    Statement = vm.Statement,
                    ImageUrl = testimonial.ImageUrl
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(testimonial.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(testimonial.ImageUrl, "testimonials");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "testimonials");
                    dto.ImageUrl = imageUrl;
                }
                await _testimonialService.EditAsync(dto);
                _notyfService.Success("Testimonial updated successfully");
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
                var testimonial = await _testimonialRepository.GetByIdAsync(id);
                if (testimonial.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(testimonial.ImageUrl, "testimonials");
                }
                await _testimonialService.DeleteAsync(id);
                _notyfService.Success("Testimonial deleted successfully");
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
                await _testimonialService.ToggleStatusAsync(id);
                _notyfService.Success("Testimonial status updated successfully");
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