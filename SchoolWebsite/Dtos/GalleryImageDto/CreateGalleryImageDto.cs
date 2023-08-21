using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.Dtos.GalleryImageDto
{
    public class CreateGalleryImageDto
    {
        public string? ImageUrl { get; set; }
        public int GalleryId { get; set; }
        public string? Description { get; set; }
    }
}