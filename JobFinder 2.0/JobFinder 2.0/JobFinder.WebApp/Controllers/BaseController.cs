using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsAuthenticated =>
            !string.IsNullOrEmpty(Request.Cookies["jwt"]);

        protected string? UserRole =>
             Request.Cookies["role"];


        protected string? UserId =>
            Request.Cookies["userid"];

        protected int? FirmId
        {
            get
            {
                if (int.TryParse(Request.Cookies["firmid"], out var id))
                    return id;

                return null;
            }
        }


        protected bool IsAdmin => UserRole == "Admin";
        protected bool IsEmployer => UserRole == "Employer";
        protected bool IsEmployee => UserRole == "Employee";
    }
}
