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
    public class LogRepository : ILogRepository
    {

        private readonly JobFinderContext _context;

        public LogRepository(JobFinderContext context)
        {
            _context = context;
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

        public async Task<List<Log>> GetAllAsync()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task<List<Log>> GetAllSearchAsync(string? search, int page, int pageSize)
        {
            var query = _context.Logs.AsQueryable();
               

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.Level.Contains(search));
            }

                return await query
                   .OrderBy(f => f.Level)
                  .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
