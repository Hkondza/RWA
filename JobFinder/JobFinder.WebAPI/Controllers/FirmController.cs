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
            if (id != firm.IDFirm)
                return BadRequest();

            _context.Entry(firm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Firms.Any(f => f.IDFirm == id))
                    return NotFound();

                throw;
            }

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
    }
}
