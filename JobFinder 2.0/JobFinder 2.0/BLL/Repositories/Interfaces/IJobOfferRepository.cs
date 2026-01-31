using DAL.Models;

namespace BLL.Repositories.Interfaces
{
    public interface IJobOfferRepository
    {
        Task<List<JobOffer>> GetActiveAsync();

        Task<List<JobOffer>> GetAllByFirmAsync(int firmId);
        Task<JobOffer?> GetByIdAsync(int id);
        Task<JobOffer> CreateAsync(JobOffer offer);
        Task<List<JobOffer>> GetAllSearchAsync(string? search, int page, int pageSize);
        Task<int> CountAsync(string? search);




    }
}
