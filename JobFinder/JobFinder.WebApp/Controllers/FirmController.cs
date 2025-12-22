using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class FirmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
