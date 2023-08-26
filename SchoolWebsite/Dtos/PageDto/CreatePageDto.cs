using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Dtos.PageDto
{
    public class CreatePageDto
    {
        public string? Name { get; set; }
        public int PageCategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool HomePage { get; set; }
    }
}