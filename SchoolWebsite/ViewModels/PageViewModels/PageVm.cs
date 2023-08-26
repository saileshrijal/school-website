namespace SchoolWebsite.ViewModels.PageViewModels
{
    public class PageVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool HomePage { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}