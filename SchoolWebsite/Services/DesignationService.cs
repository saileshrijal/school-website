using SchoolWebsite.Dtos.DesignationDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DesignationService(IDesignationRepository designationRepository, 
                                    IUnitOfWork unitOfWork)
        {
            _designationRepository = designationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateDesignationDto createDesignationDto)
        {
            var designation = new Designation()
            {
                Name = createDesignationDto.Name,
                Position = createDesignationDto.Position,
                Status = true,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(designation);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var designation = await _designationRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(designation);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditDesignationDto editDesignationDto)
        {
            var designation = await _designationRepository.GetByIdAsync(editDesignationDto.Id);
            designation.Name = editDesignationDto.Name;
            designation.Position = editDesignationDto.Position;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var designation = await _designationRepository.GetByIdAsync(id);
            designation.Status = !designation.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}