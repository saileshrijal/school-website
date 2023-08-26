using SchoolWebsite.Dtos.PageDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreatePageDto createPageDto)
        {
            var page = new Page()
            {
                Name = createPageDto.Name,
                PageCategoryId = createPageDto.PageCategoryId,
                ImageUrl = createPageDto.ImageUrl,
                Description = createPageDto.Description,
                HomePage = false,
                Status = true,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(page);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var page = await _pageRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(page);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditPageDto editPageDto)
        {
            var page = await _pageRepository.GetByIdAsync(editPageDto.Id);
            page.Name = editPageDto.Name;
            page.PageCategoryId = editPageDto.PageCategoryId;
            page.ImageUrl = editPageDto.ImageUrl;
            page.Description = editPageDto.Description;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleHomePageAsync(int id)
        {
            //toggle current page and set other pages to false
            var pages = await _pageRepository.GetAllAsync();
            foreach (var page in pages)
            {
                if (page.Id == id)
                {
                    page.HomePage = !page.HomePage;
                }
                else
                {
                    page.HomePage = false;
                }
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var page = await _pageRepository.GetByIdAsync(id);
            page.Status = !page.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}