namespace SchoolWebsite.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string? WebsiteUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}