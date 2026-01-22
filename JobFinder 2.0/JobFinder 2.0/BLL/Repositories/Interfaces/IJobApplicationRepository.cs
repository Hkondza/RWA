using DAL.Models;

namespace BLL.Repositories.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<bool> ExistsAsync(int jobOfferId, int userId);
        Task<JobApplication> CreateAsync(JobApplication application);
        Task<List<JobApplication>> GetByOfferAsync(int jobOfferId);
        Task<List<JobApplication>> GetByApplicationAsync(int jobApplicationId);
        Task<List<JobApplication>> GetByUserAsync(int userId);

        Task<List<JobApplication>> GetByFirmAsync(int firmId);
    }
}
