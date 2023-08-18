namespace SchoolWebsite.ViewModels.FaqViewModels
{
    public class FaqVm
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}