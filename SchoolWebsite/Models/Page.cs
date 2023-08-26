namespace SchoolWebsite.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PageCategoryId { get; set; }
        public PageCategory? PageCategory { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool HomePage { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}