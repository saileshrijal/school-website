using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWebsite.ViewModels.TestimonialViewModels
{
    public class EditTestimonialVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Organization is required")]
        public string? Organization { get; set; }

        [Required(ErrorMessage = "Statement is required")]
        public string? Statement { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }
        public List<SelectListItem>? DesignationSelectList { get; set; }
    }
}