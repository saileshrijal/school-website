using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebsite.Dtos.CourseDto;
using SchoolWebsite.Helpers.Interface;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;
using SchoolWebsite.ViewModels.CourseViewModels;

namespace SchoolWebsite.Areas.cms.Controllers
{
    [Area("cms")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        private readonly IFacultyRepository _facultyRepository;
        private readonly INotyfService _notyfService;
        private readonly IFileHelper _fileHelper;
        public CourseController(ICourseRepository courseRepository, ICourseService courseService, IFacultyRepository facultyRepository, INotyfService notyfService, IFileHelper fileHelper)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
            _facultyRepository = facultyRepository;
            _notyfService = notyfService;
            _fileHelper = fileHelper;
        }
        public async Task<IActionResult> Index(string? search)
        {
            var courses = new List<Course>();
            if(string.IsNullOrEmpty(search))
            {
                courses = await _courseRepository.GetAllCoursesWithFacultyAsync();
            }
            else
            {
                courses = await _courseRepository.GetAllCoursesWithFacultyAsync(search);
            }
            ViewBag.Search = search;
            var vm = courses.Select(x => new CourseVm()
            {
                Id = x.Id,
                Name = x.Name,
                FacultyName = x.Faculty!.Name,
                NumberOfYears = x.NumberOfYears,
                NumberOfSem = x.NumberOfSem,
                Objectives = x.Objectives,
                Scopes = x.Scopes,
                AdmissionRequirement = x.AdmissionRequirement,
                CourseOfStudy = x.CourseOfStudy,
                ImageUrl = x.ImageUrl,
                SyllabusUrl = x.SyllabusUrl,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var faculties = await _facultyRepository.GetAllAsync();
            var vm = new CreateCourseVm()
            {
                FacultySelectList = faculties.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var faculties = await _facultyRepository.GetAllAsync();
                    vm.FacultySelectList = faculties.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var dto = new CreateCourseDto()
                {
                    Name = vm.Name,
                    FacultyId = vm.FacultyId,
                    NumberOfYears = vm.NumberOfYears,
                    NumberOfSem = vm.NumberOfSem,
                    Objectives = vm.Objectives,
                    Scopes = vm.Scopes,
                    AdmissionRequirement = vm.AdmissionRequirement,
                    CourseOfStudy = vm.CourseOfStudy,
                };
                if (vm.Image != null)
                {
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "courses");
                    dto.ImageUrl = imageUrl;
                }
                if (vm.Syllabus != null)
                {
                    var syllabusUrl = await _fileHelper.UploadFileAsync(vm.Syllabus, "courses");
                    dto.SyllabusUrl = syllabusUrl;
                }
                await _courseService.CreateAsync(dto);
                _notyfService.Success("Course created successfully");
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
                var course = await _courseRepository.GetCourseWithFacultyAsync(id);
                var faculties = await _facultyRepository.GetAllAsync();
                var vm = new EditCourseVm()
                {
                    Id = course.Id,
                    Name = course.Name,
                    FacultyId = course.FacultyId,
                    NumberOfYears = course.NumberOfYears,
                    NumberOfSem = course.NumberOfSem,
                    Objectives = course.Objectives,
                    Scopes = course.Scopes,
                    AdmissionRequirement = course.AdmissionRequirement,
                    CourseOfStudy = course.CourseOfStudy,
                    ImageUrl = course.ImageUrl,
                    SyllabusUrl = course.SyllabusUrl,
                    FacultySelectList = faculties.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                        Selected = x.Id == course.FacultyId
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
        public async Task<IActionResult> Edit(EditCourseVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var faculties = await _facultyRepository.GetAllAsync();
                    vm.FacultySelectList = faculties.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
                    return View(vm);
                }
                var course = await _courseRepository.GetByIdAsync(vm.Id);
                var dto = new EditCourseDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    FacultyId = vm.FacultyId,
                    NumberOfYears = vm.NumberOfYears,
                    NumberOfSem = vm.NumberOfSem,
                    Objectives = vm.Objectives,
                    Scopes = vm.Scopes,
                    AdmissionRequirement = vm.AdmissionRequirement,
                    CourseOfStudy = vm.CourseOfStudy,
                    ImageUrl = course.ImageUrl,
                    SyllabusUrl = course.SyllabusUrl,
                };
                if (vm.Image != null)
                {
                    if (!string.IsNullOrEmpty(course.ImageUrl))
                    {
                        await _fileHelper.DeleteFileAsync(course.ImageUrl, "courses");
                    }
                    var imageUrl = await _fileHelper.UploadFileAsync(vm.Image, "courses");
                    dto.ImageUrl = imageUrl;
                }
                if (vm.Syllabus != null)
                {
                    if (!string.IsNullOrEmpty(course.SyllabusUrl))
                    {
                        await _fileHelper.DeleteFileAsync(course.SyllabusUrl, "courses");
                    }
                    var syllabusUrl = await _fileHelper.UploadFileAsync(vm.Syllabus, "courses");
                    dto.SyllabusUrl = syllabusUrl;
                }
                await _courseService.EditAsync(dto);
                _notyfService.Success("Course updated successfully");
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
                var course = await _courseRepository.GetByIdAsync(id);
                if (course.ImageUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(course.ImageUrl, "courses");
                }
                if (course.SyllabusUrl != null)
                {
                    await _fileHelper.DeleteFileAsync(course.SyllabusUrl, "courses");
                }
                await _courseService.DeleteAsync(id);
                _notyfService.Success("Course deleted successfully");
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
                await _courseService.ToggleStatusAsync(id);
                _notyfService.Success("Course status updated successfully");
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