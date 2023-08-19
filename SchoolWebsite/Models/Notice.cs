namespace SchoolWebsite.Models
{
    public class Notice
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool PopUp { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}