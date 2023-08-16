using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.DesignationViewModels
{
    public class CreateDesignationVm
    {
        [Required(ErrorMessage = "Designation name is required")]
        public string? Name { get; set; }
        public int Position { get; set; }
    }
}