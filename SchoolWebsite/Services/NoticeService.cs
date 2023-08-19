using System.Net.Mime;
using SchoolWebsite.Dtos.NoticeDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class NoticeService : INoticeService
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NoticeService(INoticeRepository noticeRepository, IUnitOfWork unitOfWork)
        {
            _noticeRepository = noticeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateNoticeDto createNoticeDto)
        {
            var notice = new Notice
            {
                Title = createNoticeDto.Title,
                Description = createNoticeDto.Description,
                PopUp = createNoticeDto.PopUp,
                ExpiryDate = createNoticeDto.ExpiryDate,
                ImageUrl = createNoticeDto.ImageUrl,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(notice);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(notice);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditNoticeDto editNoticeDto)
        {
            var notice = await _noticeRepository.GetByIdAsync(editNoticeDto.Id);
            notice.Title = editNoticeDto.Title;
            notice.Description = editNoticeDto.Description;
            notice.PopUp = editNoticeDto.PopUp;
            notice.ExpiryDate = editNoticeDto.ExpiryDate;
            notice.ImageUrl = editNoticeDto.ImageUrl;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var notice = await _noticeRepository.GetByIdAsync(id);
            notice.Status = !notice.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}