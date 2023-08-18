namespace SchoolWebsite.Models
{
    public class PageCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public PageCategory? Parent { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}