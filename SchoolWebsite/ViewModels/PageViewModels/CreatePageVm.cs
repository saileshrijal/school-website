using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.PageViewModels
{
    public class CreatePageVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public int PageCategoryId { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile? Image { get; set; }
        public List<SelectListItem>? CategorySelectList { get; set; }
    }
}