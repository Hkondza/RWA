using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories.Interfaces;

namespace BLL.Repositories
{
    public class UserFirmRepository : IUserFirmRepository
    {
        private readonly JobFinderContext _context;

        public UserFirmRepository(JobFinderContext context)
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
                .FirstOrDefaultAsync(uf => uf.IduserFirm == id);
        }

        public async Task<UserFirm?> GetPendingByUserAsync(int userId)
        {
            return await _context.UserFirms
                .FirstOrDefaultAsync(uf =>
                    uf.UserId == userId &&
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
