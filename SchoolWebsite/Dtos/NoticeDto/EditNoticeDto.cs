using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Dtos.NoticeDto
{
    public class EditNoticeDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool PopUp { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}