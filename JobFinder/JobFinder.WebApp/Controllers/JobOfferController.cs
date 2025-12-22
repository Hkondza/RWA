using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class JobOfferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
