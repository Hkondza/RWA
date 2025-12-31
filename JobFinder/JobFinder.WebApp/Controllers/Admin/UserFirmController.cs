using System.Text;
using System.Text.Json;
using JobFinder.WebApp.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebApp.Controllers.Admin
{
   
    public class UserFirmController : BaseController
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public UserFirmController(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_config["ApiSettings:BaseUrl"])
            };
        }

        // 📋 LISTA PENDING ZAHTJEVA
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated || !IsAdmin)
                return Unauthorized();

            // 🔐 JWT iz cookie-ja
            var jwt = Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

            var response = await _client.GetAsync("api/admin/user-firm/pending");

            if (!response.IsSuccessStatusCode)
                return View(new List<UserFirmAdminVM>());

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<UserFirmAdminVM>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(data);
        }

        // ✅ APPROVE
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            if (!IsAuthenticated || !IsAdmin)
                return Unauthorized();

            var jwt = Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

            var body = JsonSerializer.Serialize(new { userFirmId = id });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            await _client.PostAsync("api/admin/user-firm/approve", content);

            return RedirectToAction(nameof(Index));
        }

        // ❌ REJECT
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            if (!IsAuthenticated || !IsAdmin)
                return Unauthorized();

            var jwt = Request.Cookies["jwt"];
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

            var body = JsonSerializer.Serialize(new { userFirmId = id });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            await _client.PostAsync("api/admin/user-firm/reject", content);

            return RedirectToAction(nameof(Index));
        }
    }
}
