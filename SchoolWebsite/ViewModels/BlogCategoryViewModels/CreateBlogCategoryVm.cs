using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.BlogCategoryViewModels
{
    public class CreateBlogCategoryVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }

        public List<SelectListItem>? BlogCategorySelectList { get; set; }
    }
}