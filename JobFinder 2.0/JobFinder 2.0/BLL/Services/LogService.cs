using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Repositories.Interfaces;
using BLL.Services.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class LogService : ILogService
    {

        private readonly ILogRepository _repo;

        public LogService(ILogRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CountAsync(string? search)
        {
            return await _repo.CountAsync(search);
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<List<Log>> GetAllSearchAsync(string? search, int page, int pageSize)
        {
           return await _repo.GetAllSearchAsync(search, page, pageSize);    
        }
    }
}
