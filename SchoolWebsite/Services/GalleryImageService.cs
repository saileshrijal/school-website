using SchoolWebsite.Dtos.GalleryImageDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly IGalleryImageRepository _galleryImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GalleryImageService(IGalleryImageRepository galleryImageRepository,
                                    IUnitOfWork unitOfWork)
        {
            _galleryImageRepository = galleryImageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateGalleryImageDto createGalleryImageDto)
        {
            var galleryImage = new GalleryImage()
            {
                GalleryId = createGalleryImageDto.GalleryId,
                ImageUrl = createGalleryImageDto.ImageUrl,
                Slider = false,
                Status = true
            };
            await _unitOfWork.CreateAsync(galleryImage);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var galleryImage = await _galleryImageRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(galleryImage);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditGalleryImageDto editGalleryImageDto)
        {
            var galleryImage = await _galleryImageRepository.GetByIdAsync(editGalleryImageDto.Id);
            galleryImage.GalleryId = editGalleryImageDto.GalleryId;
            galleryImage.ImageUrl = editGalleryImageDto.ImageUrl;
            galleryImage.Description = editGalleryImageDto.Description;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleSliderStatusAsync(int id)
        {
            var galleryImage = await _galleryImageRepository.GetByIdAsync(id);
            galleryImage.Slider = !galleryImage.Slider;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var galleryImage = await _galleryImageRepository.GetByIdAsync(id);
            galleryImage.Status = !galleryImage.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}