using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ILogService
    {
        Task<List<Log>> GetAllAsync();
        Task<List<Log>> GetAllSearchAsync(string? search, int page, int pageSize);
        Task<int> CountAsync(string? search);
    }
}
