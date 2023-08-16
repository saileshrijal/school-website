using SchoolWebsite.Dtos.GalleryDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GalleryService(IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        {
            _galleryRepository = galleryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateGalleryDto createGalleryDto)
        {
            var gallery = new Gallery
            {
                Name = createGalleryDto.Name,
                Description = createGalleryDto.Description,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(gallery);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gallery = await _galleryRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(gallery);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditGalleryDto editGalleryDto)
        {
            var gallery = await _galleryRepository.GetByIdAsync(editGalleryDto.Id);
            gallery.Name = editGalleryDto.Name;
            gallery.Description = editGalleryDto.Description;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var faculty = await _galleryRepository.GetByIdAsync(id);
            faculty.Status = !faculty.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}