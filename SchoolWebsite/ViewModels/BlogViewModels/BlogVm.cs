using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.ViewModels.BlogViewModels
{
    public class BlogVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AuthorName { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}