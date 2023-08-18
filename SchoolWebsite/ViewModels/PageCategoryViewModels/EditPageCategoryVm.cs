using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.PageCategoryViewModels
{
    public class EditPageCategoryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }

         public List<SelectListItem>? PageCategorySelectList { get; set; }
    }
}