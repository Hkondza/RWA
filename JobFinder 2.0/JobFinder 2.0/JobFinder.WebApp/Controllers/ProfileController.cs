using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using JobFinder.WebApp.ViewModels.Profile;
using System.Runtime.CompilerServices;
using BLL.Services.Interfaces;
using AutoMapper;
using System.Runtime.InteropServices;
using BLL.DTOs.Profile;

namespace JobFinder.WebApp.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        private readonly IFirmService _firmService;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileService, IFirmService firmService, IMapper mapper)
        {
            _profileService = profileService;
            _firmService = firmService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated)
                return RedirectToAction("Login", "Account");

            //AttachJwt();


            //nemoze biti nul jer gore trazi autnetifikaciju
            var profile = await _profileService.GetMeAsync(int.Parse(UserId));
            var vm = _mapper.Map<ProfileVM>(profile);

           
            var firms = await _firmService.GetAllAsync();
            var firmConverter = _mapper.Map<List<FirmLookupVM>>(firms);

                vm.Firms = firmConverter.Select(f => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = f.IDFirm.ToString(),
                    Text = f.FirmName
                }).ToList();
            

            return View(vm);
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateVM vm)
        {
            //AttachJwt();

            if (!vm.Email.Contains("@"))
            {
                return BadRequest();
            }

            var converter = _mapper.Map<ProfileUpdateDto>(vm);

            await _profileService.UpdateAsync(int.Parse(UserId), converter);

          
            //if (!res.IsSuccessStatusCode)
            //    return BadRequest();

            return Ok();
        }

        
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM vm)
        {
            //AttachJwt();

            var converter = _mapper.Map<ChangePasswordDto>(vm);

            await _profileService.ChangePasswordAsync(int.Parse(UserId), converter);

      
            //if (!res.IsSuccessStatusCode)
            //    return BadRequest(await res.Content.ReadAsStringAsync());

            return Ok();
        }

        
        [HttpPost]
        public async Task<IActionResult> RequestFirm([FromBody] FirmRequestVM vm)
        {
           // AttachJwt();

            var converter = _mapper.Map<FirmRequestDto>(vm);

            await _profileService.RequestFirmAsync(int.Parse(UserId), converter);

       
            //if (!res.IsSuccessStatusCode)
            //    return BadRequest(await res.Content.ReadAsStringAsync());

            return Ok(new { status = "Pending" });
        }

        //private void AttachJwt()
        //{
        //    var jwt = Request.Cookies["JWT"];
        //    _client.DefaultRequestHeaders.Authorization =
        //        new AuthenticationHeaderValue("Bearer", jwt);
        //}
    }
}
