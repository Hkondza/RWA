using AutoMapper;
using JobFinder.WebAPI.DTOs.UserFirm;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/user-firm")]
    [Authorize(Roles = "Admin")]
    public class AdminUserFirmController : ControllerBase
    {
        private readonly IUserFirmService _service;
        private readonly IMapper _mapper;

        public AdminUserFirmController(
            IUserFirmService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // 1️⃣ Svi pending zahtjevi
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var entities = await _service.GetPendingAsync();
            var dto = _mapper.Map<List<UserFirmReadDto>>(entities);

            return Ok(dto);
        }

        // 2️⃣ Approve
        [HttpPost("approve")]
        public async Task<IActionResult> Approve([FromBody] UserFirmActionDto dto)
        {
            await _service.ApproveAsync(dto.UserFirmId);
            return Ok();
        }

        // 3️⃣ Reject
        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] UserFirmActionDto dto)
        {
            await _service.RejectAsync(dto.UserFirmId);
            return Ok();
        }
    }
}
