using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class JobOfferController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
