using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.BlogViewModels
{
    public class EditBlogVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int BlogCategoryId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AuthorName { get; set; }
        
        public IFormFile? Image { get; set; }

        public List<SelectListItem>? CategorySelectList { get; set; }
    }
}