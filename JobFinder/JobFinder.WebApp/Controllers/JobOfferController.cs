using JobFinder.WebApp.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;
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

        // =========================
        // GET: /JobOffer
        // =========================
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("/api/joboffer");

            if (!response.IsSuccessStatusCode)
                return View(new List<JobOfferListVM>());

            var json = await response.Content.ReadAsStringAsync();

            var offers = JsonSerializer.Deserialize<List<JobOfferListVM>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(offers);
        }

        // =========================
        // GET: /JobOffer/Details/5
        // =========================
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

            ViewBag.CanApply = IsAuthenticated && IsEmployee;

            return View(offer);
        }

        // =========================
        // GET: /JobOffer/Create
        // =========================
        public async Task<IActionResult> Create()
        {

            if (FirmId == 0 || !FirmId.HasValue)
            {
                TempData["Error"] = "Ne možeš kreirati oglas jer nemaš dodijeljenu firmu. Postavi firmu u profilu.";
                return RedirectToAction("Index", "Profile");
            }

            var vm = new JobOfferCreateVM();

            FirmLookupDto? firm = null;




            if (FirmId.HasValue)
            {
                firm = await _client.GetFromJsonAsync<FirmLookupDto>(
                    $"/api/firm/{FirmId.Value}"
                );
            }

            vm.FirmName = firm?.FirmName;


            // 🔹 JobTypes
            var jobTypes = await _client.GetFromJsonAsync<List<JobTypeLookupDto>>("/api/jobtype");
            if (jobTypes != null)
            {
                vm.JobTypes = jobTypes
                    .Select(j => new SelectListItem(j.JobName, j.IDJobType.ToString()))
                    .ToList();
            }

            // 🔹 Locations
            var locations = await _client.GetFromJsonAsync<List<LocationLookupDto>>("/api/location");
            if (locations != null)
            {
                vm.Locations = locations
                    .Select(l => new SelectListItem(l.LocationName, l.IDLocation.ToString()))
                    .ToList();
            }

            return View(vm);
        }

        // =========================
        // POST: /JobOffer/Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobOfferCreateVM vm)
        {
           

            if (!vm.JobTypeID.HasValue && string.IsNullOrWhiteSpace(vm.NewJobTypeName))
                ModelState.AddModelError(nameof(vm.NewJobTypeName), "Odaberi tip posla ili upiši novi.");

            if (!vm.LocationID.HasValue && string.IsNullOrWhiteSpace(vm.NewLocationName))
                ModelState.AddModelError(nameof(vm.NewLocationName), "Odaberi lokaciju ili upiši novu.");

            if (!ModelState.IsValid)
            {
                // Ponovno napuni dropdown liste
                return await Create();
            }

            // 🔹 Payload TOČNO kakav API očekuje
            var payload = new JobOfferCreateVM
            {
              Title =  vm.Title,
               Description =  vm.Description,
               Salary = vm.Salary,

                FirmID = FirmId,
                FirmName = vm.FirmName,

                JobTypeID = vm.JobTypeID,
                NewJobTypeName = vm.NewJobTypeName,

                LocationID = vm.LocationID,
                NewLocationName = vm.NewLocationName
            };

            

            var response = await _client.PostAsJsonAsync("/api/joboffer", payload);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška pri kreiranju oglasa.");
                return await Create();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // LOOKUP DTOs (samo za dropdown)
        // =========================
   

        private class JobTypeLookupDto
        {
            public int IDJobType { get; set; }
            public string JobName { get; set; }
        }

        private class LocationLookupDto
        {
            public int IDLocation { get; set; }
            public string LocationName { get; set; }
        }

        private class FirmLookupDto
        {
            public int IDFirm { get; set; }
            public string FirmName { get; set; }
        }
    }
}
