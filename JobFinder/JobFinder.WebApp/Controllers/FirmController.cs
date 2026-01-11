using JobFinder.WebApp.ViewModels.Admin;
using JobFinder.WebApp.ViewModels.Application;
using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class FirmController : BaseController
    {

        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public FirmController(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config["ApiSettings:BaseUrl"])
            };
        }


        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("/api/joboffer/by-firm/" + FirmId);

            if (!response.IsSuccessStatusCode)
                return View(new List<JobOfferListVM>());

            var json = await response.Content.ReadAsStringAsync();

            var offers = JsonSerializer.Deserialize<List<JobOfferListVM>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(offers);
        }

        public async Task<IActionResult> Details()
        {
            var response = await _client.GetAsync("api/jobapplication/by-firm/" + FirmId+"/applied");

            if (!response.IsSuccessStatusCode)
                return View(new List<JobApplicationUsers>());

            var json = await response.Content.ReadAsStringAsync();

            var offers = JsonSerializer.Deserialize<List<JobApplicationUsers>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(offers);
        }


        // ✅ APPROVE
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var jwt = Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwt);

            var response = await _client.PutAsync(
                $"api/jobapplication/{id}/approve",
                null
            );

            return RedirectToAction(nameof(Index));
        }

        // ❌ REJECT
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            if (!IsAuthenticated || !IsEmployer)
                return Unauthorized();

            var jwt = Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

            var body = JsonSerializer.Serialize(new { userFirmId = id });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            await _client.PostAsync("api/jobapplication/reject", content);

            return RedirectToAction(nameof(Index));
        }



    }
}
