using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Repositories
{
    public class UserFirmRepository : IUserFirmRepository
    {
        private readonly JobFinderDbContext _context;

        public UserFirmRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(UserFirm userFirm)
        {
            _context.UserFirms.Add(userFirm);
            await _context.SaveChangesAsync();
        }

        public async Task<UserFirm?> GetByIdAsync(int id)
        {
            return await _context.UserFirms
                .Include(uf => uf.User)
                .Include(uf => uf.Firm)
                .FirstOrDefaultAsync(uf => uf.IDUserFirm == id);
        }

        public async Task<UserFirm?> GetPendingByUserAsync(int userId)
        {
            return await _context.UserFirms
                .FirstOrDefaultAsync(uf =>
                    uf.UserID == userId &&
                    uf.Status == "Pending");
        }

        public async Task<List<UserFirm>> GetPendingAsync()
        {
            return await _context.UserFirms
                .Include(uf => uf.User)
                .Include(uf => uf.Firm)
                .Where(uf => uf.Status == "Pending")
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
