using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email, string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
