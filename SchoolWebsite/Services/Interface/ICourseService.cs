using SchoolWebsite.Dtos.CourseDto;

namespace SchoolWebsite.Services.Interface
{
    public interface ICourseService
    {
        Task CreateAsync(CreateCourseDto createCourseDto);
        Task EditAsync(EditCourseDto editCourseDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}