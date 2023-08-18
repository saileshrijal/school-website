using SchoolWebsite.Dtos.PageCategoryDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class PageCategoryService : IPageCategoryService
    {
        private readonly IPageCategoryRepository _pageCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PageCategoryService(IPageCategoryRepository pageCategoryRepository, IUnitOfWork unitOfWork)
        {
            _pageCategoryRepository = pageCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreatePageCategoryDto createPageCategoryDto)
        {
            var pageCategory = new PageCategory
            {
                Name = createPageCategoryDto.Name,
                ParentId = createPageCategoryDto.ParentId,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(pageCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pageCategory = await _pageCategoryRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(pageCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditPageCategoryDto editPageCategoryDto)
        {
            var pageCategory = await _pageCategoryRepository.GetByIdAsync(editPageCategoryDto.Id);
            pageCategory.Name = editPageCategoryDto.Name;
            pageCategory.ParentId = editPageCategoryDto.ParentId;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var pageCategory = await _pageCategoryRepository.GetByIdAsync(id);
            pageCategory.Status = !pageCategory.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}