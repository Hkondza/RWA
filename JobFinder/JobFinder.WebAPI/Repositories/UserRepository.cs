using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JobFinderDbContext _context;

        public UserRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsAsync(string email, string username)
        {
            return _context.Users.AnyAsync(u =>
                u.Email == email || u.Username == username);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
