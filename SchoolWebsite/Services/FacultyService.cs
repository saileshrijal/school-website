using SchoolWebsite.Dtos.FacultyDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FacultyService(IFacultyRepository facultyRepository, IUnitOfWork unitOfWork)
        {
            _facultyRepository = facultyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateFacultyDto createFacultyDto)
        {
            var faculty = new Faculty
            {
                Name = createFacultyDto.Name,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(faculty);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(faculty);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditFacultyDto editFacultyDto)
        {
            var faculty = await _facultyRepository.GetByIdAsync(editFacultyDto.Id);
            faculty.Name = editFacultyDto.Name;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var faculty = await _facultyRepository.GetByIdAsync(id);
            faculty.Status = !faculty.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}