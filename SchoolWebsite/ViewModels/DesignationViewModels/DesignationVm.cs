using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.DesignationViewModels
{
    public class DesignationVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Designation name is required")]
        public string? Name { get; set; }
        public int Position { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}