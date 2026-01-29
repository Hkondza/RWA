using BLL.Repositories.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class JobTypeRepository : IJobTypeRepository
    {

        private readonly JobFinderContext _context;

        public JobTypeRepository(JobFinderContext jobFinderContext)
        {
            _context = jobFinderContext;
        }

        public async Task<JobType> CreateAsync(JobType jobType)
        {
            _context.JobTypes.Add(jobType);
            await _context.SaveChangesAsync();
            return jobType;
        }

        public async Task DeleteAsync(JobType jobType)
        {
            _context?.JobTypes.Remove(jobType);
            await _context.SaveChangesAsync();

        }

        public Task<List<JobType>> GetAllAsync()
        {
            return _context.JobTypes.ToListAsync();
        }

        public async Task<JobType?> GetByIdAsync(int id)
        {
            return await _context.JobTypes
                .FirstOrDefaultAsync(j => j.IdjobType == id);
        }

        public Task UpdateAsync(JobType jobType)
        {
           _context?.JobTypes.Update(jobType);
            return _context.SaveChangesAsync();
        }
    }
}
