using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmController : ControllerBase
    {
        private readonly JobFinderDbContext _context;

        public FirmController(JobFinderDbContext context)
        {
            _context = context;
        }

        // GET: api/firm
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var firms = await _context.Firms.ToListAsync();
            return Ok(firms);
        }

        // GET: api/firm/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var firm = await _context.Firms.FindAsync(id);

            if (firm == null)
                return NotFound();

            return Ok(firm);
        }

        // POST: api/firm
        [HttpPost]
        public async Task<IActionResult> Create(Firm firm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Firms.Add(firm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = firm.IDFirm }, firm);
        }

        // PUT: api/firm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Firm firm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != firm.IDFirm)
                return BadRequest("ID u URL-u i ID u bodyju se ne podudaraju.");

            var exists = await _context.Firms.AnyAsync(f => f.IDFirm == id);
            if (!exists)
                return NotFound();

            _context.Entry(firm).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/firm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var firm = await _context.Firms.FindAsync(id);

            if (firm == null)
                return NotFound();

            _context.Firms.Remove(firm);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/firm/search?term=test&page=1&pageSize=10
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            string? term,
            int page = 1,
            int pageSize = 10)
        {
            var query = _context.Firms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(f =>
                    f.FirmName.Contains(term) ||
                    (f.Description != null && f.Description.Contains(term)));
            }

            var totalCount = await query.CountAsync();

            var firms = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = firms
            });
        }

    }
}
