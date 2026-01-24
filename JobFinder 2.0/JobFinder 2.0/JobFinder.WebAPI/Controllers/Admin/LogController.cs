using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    public class LogsController : ControllerBase
    {
        private readonly JobFinderContext _context;

        public LogsController(JobFinderContext context)
        {
            _context = context;
        }

        
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

        
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _context.Logs.CountAsync();
            return Ok(count);
        }
    }
}
