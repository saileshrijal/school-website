using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.BlogViewModels
{
    public class CreateBlogVm
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int BlogCategoryId { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        public string? AuthorName { get; set; }

        public IFormFile? Image { get; set; }
        public List<SelectListItem>? CategorySelectList { get; set; }
    }
}