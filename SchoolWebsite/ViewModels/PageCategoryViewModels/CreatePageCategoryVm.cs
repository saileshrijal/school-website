using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.PageCategoryViewModels
{
    public class CreatePageCategoryVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public int? ParentId { get; set; }

        public List<SelectListItem>? PageCategorySelectList { get; set; }
    }
}