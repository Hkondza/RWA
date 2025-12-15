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
                .Where(o => o.IsActive)
                .ToListAsync();
        }

        public Task<JobOffer?> GetByIdAsync(int id)
        {
            return _context.JobOffers.FindAsync(id).AsTask();
        }

        public async Task<JobOffer> CreateAsync(JobOffer offer)
        {
            _context.JobOffers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }
    }
}
