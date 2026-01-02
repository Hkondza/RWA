using JobFinder.WebAPI.DTOs.Profile;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(idStr))
                throw new Exception("JWT nema user id claim.");

            return int.Parse(idStr);
        }

        // GET: api/profile/me
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = GetUserId();
            return Ok(await _service.GetMeAsync(userId));
        }

        // PUT: api/profile/update
        [HttpPut("update")]
        public async Task<IActionResult> Update(ProfileUpdateDto dto)
        {
            var userId = GetUserId();
            await _service.UpdateAsync(userId, dto);
            return Ok();
        }

        // PUT: api/profile/change-password
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userId = GetUserId();
            await _service.ChangePasswordAsync(userId, dto);
            return Ok();
        }

        // POST: api/profile/request-firm
        [HttpPost("request-firm")]
        public async Task<IActionResult> RequestFirm(FirmRequestDto dto)
        {
            var userId = GetUserId();
            await _service.RequestFirmAsync(userId, dto);
            return Ok();
        }
    }
}
