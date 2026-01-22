using JobFinder.WebAPI.DTOs.Worker;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {

        private readonly IWorkerService _service;
        public WorkerController(IWorkerService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> StartWork(WorkerCreateDto dto)
        {
            try
            {
                 var result = await _service.CreateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("by-offer/{jobOfferId}/working")]
        public async Task<IActionResult> GetByApplicationWorking(int jobOfferId)
        {
            return Ok(await _service.GetByWorkingAsync(jobOfferId));
        }


        [HttpGet("by-offer/{jobOfferId}/finished")]
        public async Task<IActionResult> GetByOfferFinished(int jobOfferId)
        {
            return Ok(await _service.GetByFinishedAsync(jobOfferId));
        }


        //nesmi bit by offer jer imas vise offera sa istim id
        //moras stavit by application jer ti to reprezentira samo taj applicaiton


        [HttpPut("by-application/{id}/finish")]
        public async Task<IActionResult> Finish(int id)
        {
            await _service.FinishAsync(id);
            return NoContent();
        }



    }
}

