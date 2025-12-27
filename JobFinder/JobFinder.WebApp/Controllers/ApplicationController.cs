using JobFinder.WebApp.ViewModels.Application;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class ApplicationController : BaseController
    {

        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public ApplicationController(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config["ApiSettings:BaseUrl"])
            };
        }


        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Apply(JobApplicationVM vm)
        {
            // 1. mora biti login
            if (!IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // 2. mora biti Employee
            if (!IsEmployee)
                return Forbid();

            if (!ModelState.IsValid)
                return RedirectToAction("Details", "JobOffer", new { id = vm.JobOfferID });

            string nesto = UserId;

            // 3. UserID NE DOLAZI S FRONTENDA
            vm.UserID = int.Parse(UserId); // iz BaseController-a

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/jobapplication", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Prijava na posao nije uspjela.";
                return RedirectToAction("Details", "JobOffer", new { id = vm.JobOfferID });
            }

            TempData["Success"] = "Uspješno ste se prijavili na posao.";
            return RedirectToAction("MyApplications");
        }

    }


}
