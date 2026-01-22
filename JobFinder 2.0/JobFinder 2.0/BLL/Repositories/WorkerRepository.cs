using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories.Interfaces;

namespace BLL.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {

        private readonly JobFinderContext _context;

        public WorkerRepository(JobFinderContext context)
        {
            _context = context;
        }

        public async Task<Worker> CreateAsync(Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<bool> ExistsAsync(int jobapplicationId)
        {
            return await _context.JobApplications.AnyAsync(a =>
                a.IdjobApplication == jobapplicationId);
        }

        public async Task<List<Worker>> GetAllAsync()
        {
            return await _context.Workers
              .Include(a => a.JobApplication)
                  .ThenInclude(o => o.User)
                  .ToListAsync();
        }

        public async Task<List<Worker>> GetAllByJobOfferAsync(int jobOfferId)
        {
            return await _context.Workers
            .Include(a => a.JobApplication)
                .ThenInclude(o => o.User)
            .Include(a => a.JobApplication)
                .ThenInclude(a => a.JobOffer)
                .Where(w => w.JobApplication.JobOfferId == jobOfferId)
                .ToListAsync();
        }

        public async Task<Worker?> GetByApplicationIdAsync(int jobapplicationId)
        {
            return await _context.Workers
                .Include(a => a.JobApplication)
                .ThenInclude(o => o.User)
                .FirstOrDefaultAsync(w => w.JobApplicationId == jobapplicationId);
                
        }

        public async Task<Worker?> GetByIdAsync(int workerId)
        {
            return await _context.Workers
              .Include(a => a.JobApplication)
              .ThenInclude(o => o.User)
              .FirstOrDefaultAsync(o => o.Idworker == workerId);
              
        }
    }
}
