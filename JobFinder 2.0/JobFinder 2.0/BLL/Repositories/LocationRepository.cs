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
    internal class LocationRepository : ILocationRepository
    {

        private readonly JobFinderContext _context;

        public LocationRepository(JobFinderContext context)
        {
            _context = context;
        }

        public async Task<Location> CreateAsync(Location location)
        {
            _context.Locations.Add(location);
             await _context.SaveChangesAsync();
            return location;
        }

        public async Task DeleteAsync(Location location)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(int id)
        {
            return await _context.Locations
                .FirstOrDefaultAsync(l => l.Idlocation == id);
        }

        public async Task UpdateAsync(Location location)
        {
           _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }
    }
}
