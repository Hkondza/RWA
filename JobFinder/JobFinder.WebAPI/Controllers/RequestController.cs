using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public RequestController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/request
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requests = await _context.Requests.ToListAsync();
            return Ok(requests);
        }

        // GET: api/request/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
                return NotFound();

            return Ok(request);
        }

        // POST: api/request
        [HttpPost]
        public async Task<IActionResult> Create(Request request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Provjera postojanja Usera
            if (!await _context.Users.AnyAsync(u => u.IDUser == request.UserID))
                return BadRequest("User ne postoji.");

            // Provjera postojanja Firme
            if (!await _context.Firms.AnyAsync(f => f.IDFirm == request.FirmID))
                return BadRequest("Firma ne postoji.");

            // Provjera postojanja Lokacije
            if (!await _context.Locations.AnyAsync(l => l.IDLocation == request.LocationID))
                return BadRequest("Lokacija ne postoji.");

            // Poslovno pravilo:
            // isti user + ista firma + ista lokacija
            // ne smije postojati aktivni request
            var exists = await _context.Requests.AnyAsync(r =>
                r.UserID == request.UserID &&
                r.FirmID == request.FirmID &&
                r.LocationID == request.LocationID &&
                r.Status != "Completed" &&
                r.Status != "Rejected");

            if (exists)
                return BadRequest("Već postoji aktivan zahtjev za ovu firmu i lokaciju.");

            request.Status = "Created";
            request.CreatedAt = DateTime.Now;

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = request.IDRequest },
                request);
        }

        // PUT: api/request/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
                return NotFound();

            var allowedStatuses = new[] { "Created", "Accepted", "Rejected", "Completed" };
            if (!allowedStatuses.Contains(status))
                return BadRequest("Neispravan status.");

            request.Status = status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/request/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
                return NotFound();

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
