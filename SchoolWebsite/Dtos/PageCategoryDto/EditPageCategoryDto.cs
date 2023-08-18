namespace SchoolWebsite.Dtos.PageCategoryDto
{
    public class EditPageCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
    }
}