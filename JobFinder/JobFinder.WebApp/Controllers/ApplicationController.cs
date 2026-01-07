using JobFinder.WebApp.ViewModels.Application;
using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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


        public async Task<IActionResult> Index()
        {

           var response = await _client.GetAsync("api/jobapplication/by-user/" + UserId);
            

            if (!response.IsSuccessStatusCode)
                return View(new List<JobApplicationListVM>());

            var json = await response.Content.ReadAsStringAsync();

            var offers = JsonSerializer.Deserialize<List<JobApplicationListVM>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(offers);
        }


        
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


            try
            {
                vm.UserID = int.Parse(UserId);
            }
            catch (Exception)
            {
                throw new Exception("Problem u cookieu");
               
            }

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/jobapplication", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Prijava na posao nije uspjela.";
                return RedirectToAction("Details", "JobOffer", new { id = vm.JobOfferID });
            }

            TempData["Success"] = "Uspješno ste se prijavili na posao.";
            return RedirectToAction("");
        }


        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"api/jobapplication/by-application/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();

            var offer = JsonSerializer.Deserialize<List<JobApplicationDetailsVM>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );


            var application = offer.FirstOrDefault();

            if (application == null)
                return NotFound();

            return View(application);

        }

    }


}
