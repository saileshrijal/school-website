namespace SchoolWebsite.ViewModels.TestimonialViewModels
{
    public class TestimonialVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public string? Organization { get; set; }
        public string? Statement { get; set; }
        public string? ImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}