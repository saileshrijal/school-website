namespace SchoolWebsite.Models
{
    public class Faq
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}