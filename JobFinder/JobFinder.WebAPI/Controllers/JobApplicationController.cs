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

        
        [HttpGet("by-offer/{jobOfferId}")]
        public async Task<IActionResult> GetByOffer(int jobOfferId)
        {
            return Ok(await _service.GetByOfferAsync(jobOfferId));
        }

        [HttpGet("by-offer/{jobOfferId}/applied")]
        public async Task<IActionResult> GetByOfferApplied(int jobOfferId)
        {
            return Ok(await _service.GetByOfferAppliedAsync(jobOfferId));
        }

        [HttpGet("by-application/{jobApplicationId}")]
        public async Task<IActionResult> GetByApplication(int jobApplicationId)
        {
            return Ok(await _service.GetByApplicationAsync(jobApplicationId));
        }


       
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _service.GetByUserAsync(userId));
        }


        //treab dodati da tobijes sve koji su Applied , Accepeted .

        // kada se prihvati accepted. imas novu tab workers . u njemu mozes start job i end job.
        //start job -> minja bazu iz accepeted u working. 
        //end work -> minja bazu iz working u finnished.

        
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


       
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            await _service.RejectAsync(id);
            return NoContent();
        }



    }
}
