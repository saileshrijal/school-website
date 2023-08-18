using SchoolWebsite.Dtos.TestimonialDto;

namespace SchoolWebsite.Services.Interface
{
    public interface ITestimonialService
    {
        Task CreateAsync(CreateTestimonialDto createTestimonialDto);
        Task EditAsync(EditTestimonialDto editTestimonialDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}