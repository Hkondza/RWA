using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace JobFinder.WebAPI.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {

        private readonly JobFinderDbContext _context;

        public WorkerRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<Worker> CreateAsync(Worker worker)
        {
            _context.Worker.Add(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<bool> ExistsAsync(int jobapplicationId)
        {
            return await _context.JobApplications.AnyAsync(a =>
                a.IDJobApplication == jobapplicationId);
        }

        public async Task<List<Worker>> GetAllAsync()
        {
            return await _context.Worker
              .Include(a => a.JobApplication)
                  .ThenInclude(o => o.User)
                  .ToListAsync();
        }

        public async Task<List<Worker>> GetAllByJobOfferAsync(int jobOfferId)
        {
            return await _context.Worker
            .Include(a => a.JobApplication)
                .ThenInclude(o => o.User)
            .Include(a => a.JobApplication)
                .ThenInclude(a => a.JobOffer)
                .Where(w => w.JobApplication.JobOfferID == jobOfferId)
                .ToListAsync();
        }

        public async Task<Worker?> GetByApplicationIdAsync(int jobapplicationId)
        {
            return await _context.Worker
                .Include(a => a.JobApplication)
                .ThenInclude(o => o.User)
                .FirstOrDefaultAsync(w => w.JobApplicationId == jobapplicationId);
                
        }

        public async Task<Worker?> GetByIdAsync(int workerId)
        {
            return await _context.Worker
              .Include(a => a.JobApplication)
              .ThenInclude(o => o.User)
              .FirstOrDefaultAsync(o => o.IDWorker == workerId);
              
        }
    }
}
