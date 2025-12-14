using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public JobTypeController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/jobtype
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobTypes = await _context.JobTypes.ToListAsync();
            return Ok(jobTypes);
        }

        // GET: api/jobtype/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);

            if (jobType == null)
                return NotFound();

            return Ok(jobType);
        }

        // POST: api/jobtype
        [HttpPost]
        public async Task<IActionResult> Create(JobType jobType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.JobTypes.Add(jobType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = jobType.IDJobType },
                jobType);
        }

        // PUT: api/jobtype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobType jobType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != jobType.IDJobType)
                return BadRequest("ID u URL-u i ID u bodyju se ne podudaraju.");

            var exists = await _context.JobTypes.AnyAsync(j => j.IDJobType == id);
            if (!exists)
                return NotFound();

            _context.Entry(jobType).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/jobtype/5
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
