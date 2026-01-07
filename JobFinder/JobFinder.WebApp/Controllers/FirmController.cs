using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
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
    }
}
