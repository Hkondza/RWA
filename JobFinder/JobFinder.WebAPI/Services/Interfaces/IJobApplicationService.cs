using JobFinder.WebAPI.DTOs.JobApplication;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IJobApplicationService
    {
        Task<JobApplicationReadDto> ApplyAsync(JobApplicationCreateDto dto);
        Task<List<JobApplicationReadDto>> GetByOfferAsync(int jobOfferId);
        Task<List<JobApplicationReadDto>> GetByUserAsync(int userId);
    }
}
