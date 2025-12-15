using JobFinder.WebAPI.DTOs.JobOffer;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IJobOfferService
    {
        Task<List<JobOfferReadDto>> GetAllAsync();
        Task<JobOfferReadDto?> GetByIdAsync(int id);
        Task<JobOfferReadDto> CreateAsync(JobOfferCreateDto dto);
    }
}
