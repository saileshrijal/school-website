using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SchoolWebsite.Models;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PrimaryContact { get; set; }
    public string? SecondaryContact { get; set; }
    public string? TemporaryAddress { get; set; }
    public string? PermanentAddress { get; set; }
    public string? ImageUrl { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}
