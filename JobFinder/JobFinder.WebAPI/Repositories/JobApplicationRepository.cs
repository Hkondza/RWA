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

        public Task<bool> ExistsAsync(int jobOfferId, int userId)
        {
            return _context.JobApplications.AnyAsync(a =>
                a.JobOfferID == jobOfferId &&
                a.UserID == userId);
        }

        public async Task<JobApplication> CreateAsync(JobApplication application)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public Task<List<JobApplication>> GetByOfferAsync(int jobOfferId)
        {
            return _context.JobApplications
                .Where(a => a.JobOfferID == jobOfferId)
                .ToListAsync();
        }

        public Task<List<JobApplication>> GetByUserAsync(int userId)
        {
            return _context.JobApplications
                .Where(a => a.UserID == userId)
                .ToListAsync();
        }
    }
}
