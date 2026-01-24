using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;

namespace JobFinder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly JobFinderContext _context;

        public JobTypeController(JobFinderContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobTypes = await _context.JobTypes.ToListAsync();
            return Ok(jobTypes);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);

            if (jobType == null)
                return NotFound();

            return Ok(jobType);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(JobType jobType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.JobTypes.Add(jobType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = jobType.IdjobType},
                jobType);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobType jobType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != jobType.IdjobType)
                return BadRequest("ID u URL-u i ID u bodyju se ne podudaraju.");

            var exists = await _context.JobTypes.AnyAsync(j => j.IdjobType == id);
            if (!exists)
                return NotFound();

            _context.Entry(jobType).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);

            if (jobType == null)
                return NotFound();

            _context.JobTypes.Remove(jobType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
