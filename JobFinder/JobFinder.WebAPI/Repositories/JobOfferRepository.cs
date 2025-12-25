using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Repositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly JobFinderDbContext _context;

        public JobOfferRepository(JobFinderDbContext context)
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
                .FirstOrDefaultAsync(o => o.IDJobOffer == id);
        }


        public async Task<JobOffer> CreateAsync(JobOffer offer)
        {
            _context.JobOffers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }
    }
}
