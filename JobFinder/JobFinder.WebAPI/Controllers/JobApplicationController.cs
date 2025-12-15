using JobFinder.WebAPI.DTOs.JobApplication;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _service;

        public JobApplicationController(IJobApplicationService service)
        {
            _service = service;
        }

        // POST: api/jobapplication
        [HttpPost]
        public async Task<IActionResult> Apply(JobApplicationCreateDto dto)
        {
            try
            {
                var result = await _service.ApplyAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/jobapplication/by-offer/5
        [HttpGet("by-offer/{jobOfferId}")]
        public async Task<IActionResult> GetByOffer(int jobOfferId)
        {
            return Ok(await _service.GetByOfferAsync(jobOfferId));
        }

        // GET: api/jobapplication/by-user/5
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _service.GetByUserAsync(userId));
        }
    }
}
