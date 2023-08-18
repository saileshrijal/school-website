using SchoolWebsite.Dtos.BlogDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BlogService(IBlogRepository blogRepository, IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(CreateBlogDto createBlogDto)
        {
            var blog = new Blog()
            {
                Title = createBlogDto.Title,
                BlogCategoryId = createBlogDto.BlogCategoryId,
                Description = createBlogDto.Description,
                ImageUrl = createBlogDto.ImageUrl,
                VideoUrl = createBlogDto.VideoUrl,
                AuthorName = createBlogDto.AuthorName,
                Status = true,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(blog);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(blog);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditBlogDto editBlogDto)
        {
            var blog = await _blogRepository.GetByIdAsync(editBlogDto.Id);
            blog.Title = editBlogDto.Title;
            blog.BlogCategoryId = editBlogDto.BlogCategoryId;
            blog.Description = editBlogDto.Description;
            blog.ImageUrl = editBlogDto.ImageUrl;
            blog.VideoUrl = editBlogDto.VideoUrl;
            blog.AuthorName = editBlogDto.AuthorName;
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            blog.Status = !blog.Status;
            await _unitOfWork.SaveAsync();
        }
    }
}