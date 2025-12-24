using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class ApplicationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
