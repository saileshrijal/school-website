using SchoolWebsite.Dtos.TestimonialDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly IUnitOfWork _unitOfWork;
        public TestimonialService(ITestimonialRepository testimonialRepository, IUnitOfWork unitOfWork)
        {
            _testimonialRepository = testimonialRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(CreateTestimonialDto createTestimonialDto)
        {
            var testimonial = new Testimonial()
            {
                Name = createTestimonialDto.Name,
                DesignationId = createTestimonialDto.DesignationId,
                Organization = createTestimonialDto.Organization,
                Statement = createTestimonialDto.Statement,
                ImageUrl = createTestimonialDto.ImageUrl,
                Status = true,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(testimonial);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(testimonial);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditTestimonialDto editTestimonialDto)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(editTestimonialDto.Id);
            testimonial.Name = editTestimonialDto.Name;
            testimonial.DesignationId = editTestimonialDto.DesignationId;
            testimonial.Organization = editTestimonialDto.Organization;
            testimonial.Statement = editTestimonialDto.Statement;
            testimonial.ImageUrl = editTestimonialDto.ImageUrl;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id);
            testimonial.Status = !testimonial.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}