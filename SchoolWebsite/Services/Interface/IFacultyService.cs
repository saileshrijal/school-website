
using SchoolWebsite.Dtos.FacultyDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IFacultyService
    {
        Task CreateAsync(CreateFacultyDto createFacultyDto);
        Task EditAsync(EditFacultyDto editFacultyDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}