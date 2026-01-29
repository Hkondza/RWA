using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IJobTypeRepository
    {
        Task<JobType?> GetByIdAsync(int id);

        Task<JobType> CreateAsync(JobType jobType);

        Task UpdateAsync(JobType jobType);

        Task DeleteAsync(JobType jobType);

        Task<List<JobType>> GetAllAsync();
    }
}
