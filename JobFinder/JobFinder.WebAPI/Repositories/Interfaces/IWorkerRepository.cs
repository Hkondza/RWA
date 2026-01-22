using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Repositories.Interfaces
{
    public interface IWorkerRepository
    {

        Task<bool> ExistsAsync(int jobapplicationId);
        Task<Worker?> GetByApplicationIdAsync(int jobapplicationId);
        Task<List<Worker>> GetAllAsync();
        Task<List<Worker>> GetAllByJobOfferAsync(int jobOfferId);
        Task<Worker?> GetByIdAsync(int workerId);
        Task<Worker> CreateAsync(Worker worker);

    }
}
