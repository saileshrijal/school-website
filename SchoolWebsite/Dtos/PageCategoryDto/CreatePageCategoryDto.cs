using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Dtos.PageCategoryDto
{
    public class CreatePageCategoryDto
    {
        public string? Name { get; set; }
        public int? ParentId { get; set; }
    }
}