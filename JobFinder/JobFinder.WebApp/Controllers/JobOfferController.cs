using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class JobOfferController : BaseController
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public JobOfferController(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config["ApiSettings:BaseUrl"])
            };
        }

        // GET: /JobOffer
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("/api/joboffer");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<JobOfferListVM>());
            }

            var json = await response.Content.ReadAsStringAsync();

            var offers = JsonSerializer.Deserialize<List<JobOfferListVM>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return View(offers);
        }

        // (SLJEDEĆI KORAK)
        // GET: /JobOffer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"/api/joboffer/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();

            var offer = JsonSerializer.Deserialize<JobOfferDetailsVM>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            // ROLE LOGIKA IZ BaseController-a
            ViewBag.CanApply = IsAuthenticated && IsEmployee;

            return View(offer);
        }

    }
}
