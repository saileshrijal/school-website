using Microsoft.AspNetCore.Mvc;

namespace SchoolWebsite.Areas.Administrator.Controllers;

[Area("Administrator")]
public class HomeController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
