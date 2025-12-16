using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public LogsController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/logs/get/10
        [HttpGet("get/{n}")]
        public async Task<IActionResult> GetLast(int n)
        {
            if (n <= 0)
                return BadRequest("N mora biti veći od 0.");

            var logs = await _context.Logs
                .OrderByDescending(l => l.Timestamp)
                .Take(n)
                .ToListAsync();

            return Ok(logs);
        }

        // GET: api/logs/count
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _context.Logs.CountAsync();
            return Ok(count);
        }
    }
}
