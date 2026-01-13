using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Repositories.Interfaces
{
    public interface IJobOfferRepository
    {
        Task<List<JobOffer>> GetActiveAsync();

        Task<List<JobOffer>> GetAllByFirmAsync(int firmId);
        Task<JobOffer?> GetByIdAsync(int id);
        Task<JobOffer> CreateAsync(JobOffer offer);


    }
}
