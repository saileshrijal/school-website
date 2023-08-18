namespace SchoolWebsite.Dtos.BlogCategoryDto
{
    public class EditBlogCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }
}