using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface ILogRepository
    {

        Task<List<Log>> GetAllAsync();
        Task<List<Log>> GetAllSearchAsync(string? search, int page, int pageSize);
        Task<int> CountAsync(string? search);
    }
}
