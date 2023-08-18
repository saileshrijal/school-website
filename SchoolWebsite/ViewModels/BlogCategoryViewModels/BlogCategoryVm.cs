namespace SchoolWebsite.ViewModels.BlogCategoryViewModels
{
    public class BlogCategoryVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ParentCategoryName { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}