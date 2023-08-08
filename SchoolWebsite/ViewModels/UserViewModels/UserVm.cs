using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebsite.ViewModels.UserViewModels
{
    public class UserVm
    {
        public string? Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? TemporaryAddress { get; set; }
        public string? PrimaryContact { get; set; }
        public DateTime CreatedDate { get; set; }   
        public string? Role { get; set; }
        public bool Status { get; set; }
    }
}