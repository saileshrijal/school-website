using SchoolWebsite.Dtos.LinkDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LinkService(ILinkRepository linkRepository, IUnitOfWork unitOfWork)
        {
            _linkRepository = linkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateLinkDto createLinkDto)
        {
            var link = new Link
            {
                WebsiteUrl = createLinkDto.WebsiteUrl,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(link);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var link = await _linkRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(link);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditLinkDto editLinkDto)
        {
            var link = await _linkRepository.GetByIdAsync(editLinkDto.Id);
            link.WebsiteUrl = editLinkDto.WebsiteUrl;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var link = await _linkRepository.GetByIdAsync(id);
            link.Status = !link.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}