using SchoolWebsite.Dtos.NoticeDto;

namespace SchoolWebsite.Services.Interface
{
    public interface INoticeService
    {
        Task CreateAsync(CreateNoticeDto createNoticeDto);
        Task EditAsync(EditNoticeDto editNoticeDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}