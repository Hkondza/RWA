using JobFinder.WebAPI.DTOs.JobApplication;
using JobFinder.WebAPI.DTOs.UserFirm;
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

        [HttpGet("by-application/{jobApplicationId}")]
        public async Task<IActionResult> GetByApplication(int jobApplicationId)
        {
            return Ok(await _service.GetByApplicationAsync(jobApplicationId));
        }


        // GET: api/jobapplication/by-user/5
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _service.GetByUserAsync(userId));
        }


        //treab dodati da tobijes sve koji su Applied , Accepeted .

        // kada se prihvati accepted. imas novu tab workers . u njemu mozes start job i end job.
        //start job -> minja bazu iz accepeted u working. 
        //end work -> minja bazu iz working u finnished.

        // GET: api/jobapplication/by-firm/5
        [HttpGet("by-firm/{firmId}")]
        public async Task<IActionResult> GetByFirm(int firmId)
        {
            return Ok(await _service.GetByFirmAsync(firmId));
        }

        [HttpGet("by-firm/{firmId}/applied")]
        public async Task<IActionResult> GetByFirmApplied(int firmId)
        {
            return Ok(await _service.GetByFirmAppliedAsync(firmId));
        }


        // PUT: api/jobapplication/{id}/approve
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveAsync(id);
            return NoContent();
        }



        // 2️⃣ Approve
        [HttpPost("approve")]
        public async Task<IActionResult> Approve([FromBody] JobApplicationReadDto dto)
        {
            await _service.ApproveAsync(dto.IDJobApplication);
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
