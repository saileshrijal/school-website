namespace SchoolWebsite.Dtos.TestimonialDto
{
    public class EditTestimonialDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DesignationId { get; set; }
        public string? Organization { get; set; }
        public string? Statement { get; set; }
        public string? ImageUrl { get; set; }
    }
}