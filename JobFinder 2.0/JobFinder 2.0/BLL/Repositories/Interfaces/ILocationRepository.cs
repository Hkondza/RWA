using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location?> GetByIdAsync(int id);

        Task<Location> CreateAsync(Location location);

        Task UpdateAsync(Location location);

        Task DeleteAsync(Location location);

        Task<List<Location>> GetAllAsync();

    }
}
