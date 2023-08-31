namespace SchoolWebsite.Dtos.EventDto
{
    public class CreateEventDto
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? Organizer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }
}