namespace SchoolWebsite.ViewModels.GalleryViewModels
{
    public class GalleryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}