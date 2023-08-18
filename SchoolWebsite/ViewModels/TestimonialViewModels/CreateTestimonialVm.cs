using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.TestimonialViewModels
{
    public class CreateTestimonialVm
    {
        public string? Name { get; set; }
        public int DesignationId { get; set; }
        public string? Organization { get; set; }
        public string? Statement { get; set; }
        public IFormFile? Image { get; set; }
        public List<SelectListItem>? DesignationSelectList { get; set; }
    }
}