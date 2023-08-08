using Microsoft.AspNetCore.Identity;

namespace SchoolWebsite.Models;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PinCode { get; set; }
    public bool Status { get; set; }
    public string? FullName
    {
        get
        {
            return FirstName + "" + LastName;
        }
    }
}
