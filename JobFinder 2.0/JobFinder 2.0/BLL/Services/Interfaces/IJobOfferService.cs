using BLL.DTOs.JobOffer;

namespace BLL.Services.Interfaces
{
    public interface IJobOfferService
    {
        Task<List<JobOfferReadDto>> GetAllAsync();
        Task<JobOfferReadDto?> GetByIdAsync(int id);
        Task<JobOfferReadDto> CreateAsync(JobOfferCreateDto dto);
        Task<List<JobOfferReadDto>> GetByFirmAsync(int firmId);
    }
}
