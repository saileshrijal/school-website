using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.PageViewModels
{
    public class EditPageVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int PageCategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public List<SelectListItem>? CategorySelectList { get; set; }
    }
}