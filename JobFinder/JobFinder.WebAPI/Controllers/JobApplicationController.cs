using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public JobApplicationController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/jobapplication/by-offer/5
        [HttpGet("by-offer/{jobOfferId}")]
        public async Task<IActionResult> GetByOffer(int jobOfferId)
        {
            var apps = await _context.JobApplications
                .Where(a => a.JobOfferID == jobOfferId)
                .ToListAsync();

            return Ok(apps);
        }

        // GET: api/jobapplication/by-user/5
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var apps = await _context.JobApplications
                .Where(a => a.UserID == userId)
                .ToListAsync();

            return Ok(apps);
        }

        // POST: api/jobapplication
        [HttpPost]
        public async Task<IActionResult> Apply(JobApplication application)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _context.Users.AnyAsync(u => u.IDUser == application.UserID))
                return BadRequest("User ne postoji.");

            if (!await _context.JobOffers.AnyAsync(o =>
                o.IDJobOffer == application.JobOfferID && o.IsActive))
                return BadRequest("Oglas ne postoji ili nije aktivan.");

            // zabrana duple prijave
            var exists = await _context.JobApplications.AnyAsync(a =>
                a.JobOfferID == application.JobOfferID &&
                a.UserID == application.UserID);

            if (exists)
                return BadRequest("Već ste se prijavili na ovaj oglas.");

            application.Status = "Applied";
            application.AppliedAt = DateTime.Now;

            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            return Ok(application);
        }

        // PUT: api/jobapplication/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var app = await _context.JobApplications.FindAsync(id);

            if (app == null)
                return NotFound();

            var allowed = new[] { "Applied", "Accepted", "Rejected" };
            if (!allowed.Contains(status))
                return BadRequest("Neispravan status.");

            app.Status = status;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
