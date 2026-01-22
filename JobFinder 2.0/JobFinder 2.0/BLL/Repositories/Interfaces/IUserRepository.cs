using DAL.Models;

namespace BLL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string email, string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
