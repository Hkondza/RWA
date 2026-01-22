using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobFinderDbContext _context;

        public JobApplicationRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int jobOfferId, int userId)
        {
            return await _context.JobApplications.AnyAsync(a =>
                a.JobOfferID == jobOfferId &&
                a.UserID == userId);
        }

        public async Task<JobApplication> CreateAsync(JobApplication application)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<List<JobApplication>> GetByOfferAsync(int jobOfferId)
        {
            return await _context.JobApplications
                     .Include(a => a.JobOffer)
                         .ThenInclude(o => o.Firm)
                     .Include(a => a.JobOffer)
                         .ThenInclude(o => o.JobType)
                     .Include(a => a.JobOffer)
                         .ThenInclude(o => o.Location)
                    .Include(a => a.User)
                  .Where(a => a.JobOfferID == jobOfferId)
                  .ToListAsync();
        }

        public async Task<List<JobApplication>> GetByUserAsync(int userId)
        {
            return await _context.JobApplications
                .Include(a => a.JobOffer)
                    .ThenInclude(o => o.Firm)
                .Include(a => a.JobOffer)
                    .ThenInclude(o => o.JobType)
                .Include(a => a.JobOffer)
                    .ThenInclude(o => o.Location)
             .Where(a => a.UserID == userId)
             .ToListAsync();
               
        }

        public async Task<List<JobApplication>> GetByApplicationAsync(int jobApplicationId)
        {
            return await _context.JobApplications
                 .Include(a => a.JobOffer)
                     .ThenInclude(o => o.Firm)
                 .Include(a => a.JobOffer)
                     .ThenInclude(o => o.JobType)
                 .Include(a => a.JobOffer)
                     .ThenInclude(o => o.Location)
              .Where(a => a.IDJobApplication == jobApplicationId)
              .ToListAsync();
        }

        public async Task<List<JobApplication>> GetByFirmAsync(int firmId)
        {
            return await _context.JobApplications
               .Include(a => a.JobOffer)
                   .ThenInclude(o => o.Firm)
               .Include(a => a.JobOffer)
                   .ThenInclude(o => o.JobType)
               .Include(a => a.JobOffer)
                   .ThenInclude(o => o.Location)
            .Include(a => a.User)
            .Where(a => a.JobOffer.Firm.IDFirm == firmId)
            .ToListAsync();
        }
    }
}
