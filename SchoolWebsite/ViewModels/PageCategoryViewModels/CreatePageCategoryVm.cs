using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.PageCategoryViewModels
{
    public class CreatePageCategoryVm
    {
        public string? Name { get; set; }
        public int? ParentId { get; set; }

        public List<SelectListItem>? PageCategorySelectList { get; set; }
    }
}