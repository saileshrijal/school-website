namespace SchoolWebsite.Models
{
    public class Designation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}