using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories.Interfaces;

namespace BLL.Repositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly JobFinderContext _context;

        public JobOfferRepository(JobFinderContext context)
        {
            _context = context;
        }

        public async Task<List<JobOffer>> GetActiveAsync()
        {
            return await _context.JobOffers
                .Include(o => o.Firm)
                .Include(o => o.JobType)
                .Include(o => o.Location)
                .Where(o => o.IsActive)
                .ToListAsync();
        }

        public async Task<JobOffer?> GetByIdAsync(int id)
        {
            return await _context.JobOffers
                .Include(o => o.Firm)
                .Include(o => o.JobType)
                .Include(o => o.Location)
                .FirstOrDefaultAsync(o => o.IdjobOffer == id);
        }


        public async Task<JobOffer> CreateAsync(JobOffer offer)
        {
            _context.JobOffers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<List<JobOffer>> GetAllByFirmAsync(int firmId)
        {
            return await _context.JobOffers
               .Include(o => o.Firm)
               .Include(o => o.JobType)
               .Include(o => o.Location)
               .Where(o => o.FirmId == firmId)
               .ToListAsync();
        }

        public async Task<List<JobOffer>> GetAllSearchAsync(string? search, int page, int pageSize)
        {
            var query = _context.JobOffers
             .Include(o => o.Firm)
                .Include(o => o.JobType)
                  .Include(o => o.Location)
                      .Where(o => o.IsActive)
                         .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.Title.Contains(search));
            }

            return await query
                .OrderBy(f => f.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync(string? search)
        {
            var query = _context.JobOffers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.Title.Contains(search));
            }

            return await query.CountAsync();
        }
    }
}
