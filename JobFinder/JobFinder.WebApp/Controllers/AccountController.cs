using JobFinder.WebApp.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace JobFinder.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _http;

        public AccountController(IConfiguration config, IHttpClientFactory http)
        {
            _config = config;
            _http = http;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }


            if (!ModelState.IsValid)
                return View(vm);

            var client = _http.CreateClient();
            var apiUrl = $"{_config["ApiSettings:BaseUrl"]}/user/login";

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                vm.ErrorMessage = "Neispravni podaci za prijavu.";
                return View(vm);
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponseModel>(
                responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            // ⬇️ privremeno (kasnije cookie / auth)
            HttpContext.Session.SetString("JWT", loginResponse.Token);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var client = _http.CreateClient();
            var apiUrl = $"{_config["ApiSettings:BaseUrl"]}/user/register";

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                vm.ErrorMessage = "Registracija nije uspjela. Provjerite podatke.";
                return View(vm);
            }

            // nakon uspješne registracije → login
            return RedirectToAction(nameof(Login));
        }


    }
}
