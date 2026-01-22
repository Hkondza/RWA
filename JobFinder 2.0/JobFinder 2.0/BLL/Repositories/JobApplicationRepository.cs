using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories.Interfaces;

namespace BLL.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobFinderContext _context;

        public JobApplicationRepository(JobFinderContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int jobOfferId, int userId)
        {
            return await _context.JobApplications.AnyAsync(a =>
                a.JobOfferId == jobOfferId &&
                a.UserId == userId);
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
                  .Where(a => a.JobOfferId == jobOfferId)
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
             .Where(a => a.UserId == userId)
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
              .Where(a => a.IdjobApplication == jobApplicationId)
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
            .Where(a => a.JobOffer.Firm.Idfirm == firmId)
            .ToListAsync();
        }
    }
}
