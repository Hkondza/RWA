using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class FirmController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
