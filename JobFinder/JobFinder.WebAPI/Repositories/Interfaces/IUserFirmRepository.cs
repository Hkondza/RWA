using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Repositories.Interfaces
{
    public interface IUserFirmRepository
    {
        Task CreateAsync(UserFirm userFirm);
        Task<UserFirm?> GetByIdAsync(int id);
        Task<UserFirm?> GetPendingByUserAsync(int userId);
        Task<List<UserFirm>> GetPendingAsync();
        Task SaveChangesAsync();
    }
}
