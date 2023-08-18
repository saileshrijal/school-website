using SchoolWebsite.Dtos.CourseDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateCourseDto createCourseDto)
        {
            var course = new Course
            {
                Name = createCourseDto.Name,
                FacultyId = createCourseDto.FacultyId,
                NumberOfYears = createCourseDto.NumberOfYears,
                NumberOfSem = createCourseDto.NumberOfSem,
                Objectives = createCourseDto.Objectives,
                Scopes = createCourseDto.Scopes,
                AdmissionRequirement = createCourseDto.AdmissionRequirement,
                CourseOfStudy = createCourseDto.CourseOfStudy,
                ImageUrl = createCourseDto.ImageUrl,
                SyllabusUrl = createCourseDto.SyllabusUrl,
                Status = true,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(course);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(course);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditCourseDto editCourseDto)
        {
            var course = await _courseRepository.GetByIdAsync(editCourseDto.Id);
            course.Name = editCourseDto.Name;
            course.FacultyId = editCourseDto.FacultyId;
            course.NumberOfYears = editCourseDto.NumberOfYears;
            course.NumberOfSem = editCourseDto.NumberOfSem;
            course.Objectives = editCourseDto.Objectives;
            course.Scopes = editCourseDto.Scopes;
            course.AdmissionRequirement = editCourseDto.AdmissionRequirement;
            course.CourseOfStudy = editCourseDto.CourseOfStudy;
            course.ImageUrl = editCourseDto.ImageUrl;
            course.SyllabusUrl = editCourseDto.SyllabusUrl;
            await _unitOfWork.UpdateAsync(course);
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            course.Status = !course.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}