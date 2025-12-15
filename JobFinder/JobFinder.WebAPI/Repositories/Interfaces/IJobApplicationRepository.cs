using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Repositories.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<bool> ExistsAsync(int jobOfferId, int userId);
        Task<JobApplication> CreateAsync(JobApplication application);
        Task<List<JobApplication>> GetByOfferAsync(int jobOfferId);
        Task<List<JobApplication>> GetByUserAsync(int userId);
    }
}
