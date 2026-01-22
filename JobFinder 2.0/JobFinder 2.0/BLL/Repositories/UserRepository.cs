using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories.Interfaces;

namespace BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JobFinderContext _context;

        public UserRepository(JobFinderContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string email, string username)
        {
            return await _context.Users.AnyAsync(u =>
                u.Email == email || u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
