using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobOfferController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public JobOfferController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/joboffer?term=&locationId=&jobTypeId=&page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? term,
            int? locationId,
            int? jobTypeId,
            int page = 1,
            int pageSize = 10)
        {
            var query = _context.JobOffers
                .Where(o => o.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
                query = query.Where(o =>
                    o.Title.Contains(term) ||
                    o.Description.Contains(term));

            if (locationId.HasValue)
                query = query.Where(o => o.LocationID == locationId);

            if (jobTypeId.HasValue)
                query = query.Where(o => o.JobTypeID == jobTypeId);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = data
            });
        }

        // GET: api/joboffer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _context.JobOffers.FindAsync(id);

            if (offer == null || !offer.IsActive)
                return NotFound();

            return Ok(offer);
        }

        // POST: api/joboffer
        [HttpPost]
        public async Task<IActionResult> Create(JobOffer offer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // provjere FK
            if (!await _context.Firms.AnyAsync(f => f.IDFirm == offer.FirmID))
                return BadRequest("Firma ne postoji.");

            if (!await _context.JobTypes.AnyAsync(j => j.IDJobType == offer.JobTypeID))
                return BadRequest("JobType ne postoji.");

            if (!await _context.Locations.AnyAsync(l => l.IDLocation == offer.LocationID))
                return BadRequest("Lokacija ne postoji.");

            offer.CreatedAt = DateTime.Now;
            offer.IsActive = true;

            _context.JobOffers.Add(offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = offer.IDJobOffer },
                offer);
        }

        // PUT: api/joboffer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobOffer offer)
        {
            if (id != offer.IDJobOffer)
                return BadRequest();

            var exists = await _context.JobOffers.AnyAsync(o => o.IDJobOffer == id);
            if (!exists)
                return NotFound();

            _context.Entry(offer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/joboffer/5  (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _context.JobOffers.FindAsync(id);

            if (offer == null)
                return NotFound();

            offer.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
