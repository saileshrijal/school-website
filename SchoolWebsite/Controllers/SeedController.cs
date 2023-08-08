using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.Seeder.Interface;

namespace SchoolWebsite.Controllers
{
    public class SeedController : Controller
    {
        private readonly IUserSeeder _userSeeder;
        public SeedController(IUserSeeder userSeeder)
        {
            _userSeeder = userSeeder;
        }

        public async Task<IActionResult> SeedAdminUser()
        {
            try
            {
                await _userSeeder.SeedAdminUserAsync();
                return Ok("Admin user seeded successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}