using System.ComponentModel.DataAnnotations;

namespace SchoolWebsite.ViewModels.FaqViewModels
{
    public class EditFaqVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a question")]
        public string? Question { get; set; }

        [Required(ErrorMessage = "Please enter an answer")]
        public string? Answer { get; set; }
    }
}