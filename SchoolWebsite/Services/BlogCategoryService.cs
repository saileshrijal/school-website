using SchoolWebsite.Dtos.BlogCategoryDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IUnitOfWork unitOfWork)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var blogCategory = new BlogCategory
            {
                Name = createBlogCategoryDto.Name,
                Description = createBlogCategoryDto.Description,
                ParentId = createBlogCategoryDto.ParentId,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(blogCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(blogCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditBlogCategoryDto editBlogCategoryDto)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(editBlogCategoryDto.Id);
            blogCategory.Name = editBlogCategoryDto.Name;
            blogCategory.Description = editBlogCategoryDto.Description;
            blogCategory.ParentId = editBlogCategoryDto.ParentId;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            blogCategory.Status = !blogCategory.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}