using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.ViewModels.NoticeViewModels
{
    public class CreateNoticeVm
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        public DateTime ExpiryDate { get; set; }
        public bool PopUp { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}