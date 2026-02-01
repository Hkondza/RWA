using AutoMapper;
using BLL.DTOs.User;
using BLL.Services.Interfaces;
using JobFinder.WebApp.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;



namespace JobFinder.WebApp.Controllers
{
    public class AccountController : Controller
    {


        // pogledaj treba li dodati service u web app.
        //
        // Mozda treba jer nema izgradenih servisa
        // Mozda NE treba ako su se izgradili u BLL sa njegovim servisima

        private readonly IUserService _userService;
        private readonly  IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {

            //foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}


            if (!ModelState.IsValid)
                return View(vm);


            var converter = _mapper.Map<UserLoginDto>(vm);


            try
            {
              await _userService.LoginAsync(converter);
            }
            catch (Exception)
            {
                vm.ErrorMessage = "Neispravni podaci za prijavu.";
                return View(vm);
                
            }

            var loginResponse = await _userService.LoginAsync(converter);

        

            // JWT (HttpOnly)
            Response.Cookies.Append(
                "jwt",
                loginResponse.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                }
            );

            // USERNAME (za navbar)
            Response.Cookies.Append(
                "username",
                loginResponse.User.Username,
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                }
            );

            // ROLE (Employer / Employee / Admin)
            Response.Cookies.Append(
                "role",
                loginResponse.User.Role,
                new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(2)
                }
            );

            Response.Cookies.Append(
               "userid",
               loginResponse.User.IDUser + "",
               new CookieOptions
               {
                   HttpOnly = false,
                   Secure = true,
                   SameSite = SameSiteMode.Strict,
                   Expires = DateTimeOffset.UtcNow.AddHours(2)
               }
           );

            Response.Cookies.Append(
              "firmid",
              loginResponse.User.FirmID + "",
              new CookieOptions
              {
                  HttpOnly = false,
                  Secure = true,
                  SameSite = SameSiteMode.Strict,
                  Expires = DateTimeOffset.UtcNow.AddHours(2)
              }
          );

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

            var converter = _mapper.Map<UserRegisterDto>(vm);

            try
            {
                await _userService.RegisterAsync(converter);
            }
            catch (Exception)
            {
                vm.ErrorMessage = "Mail vec Postoji.";
                return View(vm);

            }

            var register = await _userService.RegisterAsync(converter);

    

            // nakon uspješne registracije → login
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("role");
            Response.Cookies.Delete("userid");
            Response.Cookies.Delete("firmid");

            return RedirectToAction("Login");


        }


    }
}
