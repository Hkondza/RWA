using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using JobFinder.WebApp.ViewModels.Profile;

namespace JobFinder.WebApp.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public ProfileController(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config["ApiSettings:BaseUrl"])
            };
        }

        // GET: /Profile
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated)
                return RedirectToAction("Login", "Account");

            AttachJwt();

            var profileRes = await _client.GetAsync("/api/profile/me");
            if (!profileRes.IsSuccessStatusCode)
                return RedirectToAction("Login", "Account");

            var profileJson = await profileRes.Content.ReadAsStringAsync();
            var vm = JsonSerializer.Deserialize<ProfileVM>(
                profileJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;

            // dohvat firmi za dropdown
            var firmsRes = await _client.GetAsync("/api/firm/firms");
            if (firmsRes.IsSuccessStatusCode)
            {
                var firmsJson = await firmsRes.Content.ReadAsStringAsync();
                var firms = JsonSerializer.Deserialize<List<FirmLookupVM>>(
                    firmsJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                )!;

                vm.Firms = firms.Select(f => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = f.IDFirm.ToString(),
                    Text = f.FirmName
                }).ToList();
            }

            return View(vm);
        }

        // AJAX: update profila
        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateVM vm)
        {
            AttachJwt();

            var res = await _client.PutAsJsonAsync("/api/profile/update", vm);
            if (!res.IsSuccessStatusCode)
                return BadRequest();

            return Ok();
        }

        // AJAX: promjena lozinke
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM vm)
        {
            AttachJwt();

            var res = await _client.PutAsJsonAsync("/api/profile/change-password", vm);
            if (!res.IsSuccessStatusCode)
                return BadRequest(await res.Content.ReadAsStringAsync());

            return Ok();
        }

        // AJAX: zahtjev za firmu
        [HttpPost]
        public async Task<IActionResult> RequestFirm([FromBody] FirmRequestVM vm)
        {
            AttachJwt();

            if (vm.FirmID == null || vm.FirmID == 0) 
            {
                
            }

            var res = await _client.PostAsJsonAsync("/api/profile/request-firm", vm);
            if (!res.IsSuccessStatusCode)
                return BadRequest(await res.Content.ReadAsStringAsync());

            return Ok(new { status = "Pending" });
        }

        private void AttachJwt()
        {
            var jwt = Request.Cookies["JWT"];
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwt);
        }
    }
}
