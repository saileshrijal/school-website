namespace SchoolWebsite.Dtos.GalleryImageDto
{
    public class EditGalleryImageDto
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int GalleryId { get; set; }
        public string? Description { get; set; }
    }
}