using JobFinder.WebAPI.DTOs.JobOffer;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService _service;

        public JobOfferController(IJobOfferService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) 
            {
                return BadRequest();
            }

            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobOfferCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }
    }
}
