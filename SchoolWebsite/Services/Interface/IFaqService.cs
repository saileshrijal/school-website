using SchoolWebsite.Dtos.FaqDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IFaqService
    {
        Task CreateAsync(CreateFaqDto createFaqDto);
        Task EditAsync(EditFaqDto editFaqDto);
        Task DeleteAsync(int id);
    }
}