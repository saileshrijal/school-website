using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.ViewModels.GalleryImageViewModels
{
    public class GalleryImageVm
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? GalleryName { get; set; }
        public string? Description { get; set; }
        public bool Slider { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}