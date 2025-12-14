using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public LocationController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/location
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _context.Locations.ToListAsync();
            return Ok(locations);
        }

        // GET: api/location/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
                return NotFound();

            return Ok(location);
        }

        // POST: api/location
        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = location.IDLocation },
                location);
        }

        // PUT: api/location/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Location location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != location.IDLocation)
                return BadRequest("ID u URL-u i ID u bodyju se ne podudaraju.");

            var exists = await _context.Locations.AnyAsync(l => l.IDLocation == id);
            if (!exists)
                return NotFound();

            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/location/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
                return NotFound();

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
