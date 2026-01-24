using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.Firm;
using BLL.Services.Interfaces;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmController : ControllerBase
    {
        private readonly IFirmService _service;

        public FirmController(IFirmService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page i pageSize moraju biti veći od 0.");

            var firms = await _service.GetAllSearchAsync(search, page, pageSize);
            var totalCount = await _service.CountAsync(search);

            return Ok(new
            {
                data = firms,
                totalCount,
                page,
                pageSize
            });
        }

        [HttpGet("firms")]
        public async Task<IActionResult> Get() { 
          return Ok(await _service.GetAllAsync());
        }




        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var firm = await _service.GetByIdAsync(id);
            if (firm == null)
                return NotFound();

            return Ok(firm);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(FirmCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.IDFirm },
                created
            );
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FirmUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
