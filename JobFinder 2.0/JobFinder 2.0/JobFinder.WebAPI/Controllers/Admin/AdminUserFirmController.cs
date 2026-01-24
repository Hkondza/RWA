using AutoMapper;
using BLL.DTOs.UserFirm;
using BLL.Services.Interfaces;
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

        
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var entities = await _service.GetPendingAsync();
            var dto = _mapper.Map<List<UserFirmReadDto>>(entities);

            return Ok(dto);
        }

        
        [HttpPost("approve")]
        public async Task<IActionResult> Approve([FromBody] UserFirmActionDto dto)
        {
            await _service.ApproveAsync(dto.UserFirmId);
            return Ok();
        }

        
        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] UserFirmActionDto dto)
        {
            await _service.RejectAsync(dto.UserFirmId);
            return Ok();
        }
    }
}
