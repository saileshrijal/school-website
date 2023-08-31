namespace SchoolWebsite.ViewModels.EventViewModels
{
    public class EventVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? Organizer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}