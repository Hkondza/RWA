using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Repositories
{
    public class FirmRepository : IFirmRepository
    {
        private readonly JobFinderDbContext _context;
        public FirmRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(string? search)
        {
            var query = _context.Firms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.FirmName.Contains(search));
            }

            return await query.CountAsync();
        }

        public async Task<Firm> CreateAsync(Firm firm)
        {
            _context.Firms.Add(firm);
            await _context.SaveChangesAsync();
            return firm;
        }

        public async Task DeleteAsync(Firm firm)
        {
            _context?.Firms.Remove(firm);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Firm>> GetAllAsync(string? search, int page, int pageSize)
        {
            var query = _context.Firms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.FirmName.Contains(search));
            }

            return await query
                .OrderBy(f => f.FirmName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Firm?> GetByIdAsync(int id)
        {
            return await _context.Firms
                .FirstOrDefaultAsync(f => f.IDFirm == id);
        }

        public async Task UpdateAsync(Firm firm)
        {
            _context.Firms.Update(firm);
            await _context.SaveChangesAsync();
        }
    }
}
