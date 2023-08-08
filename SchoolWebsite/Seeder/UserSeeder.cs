using System.Transactions;
using SchoolWebsite.Constants;
using SchoolWebsite.Models;
using SchoolWebsite.Seeder.Interface;
using Microsoft.AspNetCore.Identity;

namespace SchoolWebsite.Seeder
{
    public class UserSeeder : IUserSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAdminUserAsync()
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var adminUsers = await _userManager.GetUsersInRoleAsync(UserRoles.Admin);
            if (adminUsers.Any()) { throw new Exception("Admin user already exists"); }

            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            var adminUser = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "Super",
                LastName = "Admin",
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            var result = await _userManager.CreateAsync(adminUser, "Admin@0011");
            if (!result.Succeeded) { throw new Exception("Admin user creation failed"); }
            await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            tx.Complete();
        }
    }
}