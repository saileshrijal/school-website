using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Dtos.BlogDto
{
    public class EditBlogDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int BlogCategoryId { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AuthorName { get; set; }
    }
}