using SchoolWebsite.Dtos.FacultyDto;
using SchoolWebsite.Dtos.VideoDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VideoService(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
        {
            _videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateVideoDto createVideoDto)
        {
            var video = new Video
            {
                Name = createVideoDto.Name,
                VideoUrl = createVideoDto.VideoUrl,
                Description = createVideoDto.Description,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(video);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(video);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditVideoDto editVideoDto)
        {
            var video = await _videoRepository.GetByIdAsync(editVideoDto.Id);
            video.Name = editVideoDto.Name;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            video.Status = !video.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}