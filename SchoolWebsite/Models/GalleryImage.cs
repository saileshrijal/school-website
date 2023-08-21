namespace SchoolWebsite.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int GalleryId { get; set; }
        public Gallery? Gallery { get; set; }
        public string? Description { get; set; }
        public bool Slider { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}