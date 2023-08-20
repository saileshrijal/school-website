namespace SchoolWebsite.ViewModels.VideoViewModels
{
    public class VideoVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}