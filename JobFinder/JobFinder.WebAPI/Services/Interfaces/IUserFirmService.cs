using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IUserFirmService
    {
        Task CreateRequestAsync(int userId, int firmId);
        Task<List<UserFirm>> GetPendingAsync();
        Task ApproveAsync(int userFirmId);
        Task RejectAsync(int userFirmId);
    }
}
