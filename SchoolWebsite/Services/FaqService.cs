using SchoolWebsite.Dtos.FaqDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FaqService(IFaqRepository faqRepository, IUnitOfWork unitOfWork)
        {
            _faqRepository = faqRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateFaqDto createFaqDto)
        {
            var faq = new Faq(){
                Question = createFaqDto.Question,
                Answer = createFaqDto.Answer,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(faq);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faq = await _faqRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(faq);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditFaqDto editFaqDto)
        {
            var faq = await _faqRepository.GetByIdAsync(editFaqDto.Id);
            faq.Question = editFaqDto.Question;
            faq.Answer = editFaqDto.Answer;
            await _unitOfWork.SaveAsync();
        }
    }
}