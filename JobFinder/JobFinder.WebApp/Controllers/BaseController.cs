using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsAuthenticated =>
        !string.IsNullOrEmpty(HttpContext.Session.GetString("JWT"));

        protected string? UserRole =>
            HttpContext.Session.GetString("Role");

        protected int? UserId =>
            HttpContext.Session.GetInt32("UserId");

        protected bool IsAdmin => UserRole == "Admin";
        protected bool IsEmployer => UserRole == "Employer";
        protected bool IsEmployee => UserRole == "Employee";
    }
}
