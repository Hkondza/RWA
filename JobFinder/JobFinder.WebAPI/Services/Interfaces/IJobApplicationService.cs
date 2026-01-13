using JobFinder.WebAPI.DTOs.JobApplication;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IJobApplicationService
    {
        Task<JobApplicationReadDto> ApplyAsync(JobApplicationCreateDto dto);
        Task<List<JobApplicationReadDto>> GetByOfferAsync(int jobOfferId);
        Task<List<JobApplicationReadDto>> GetByOfferAppliedAsync(int jobOfferId);
        Task<List<JobApplicationReadDto>> GetByApplicationAsync(int jobApplicationId);
        Task<List<JobApplicationReadDto>> GetByUserAsync(int userId);
        Task<List<JobApplicationReadDto>> GetByFirmAsync(int firmId);
        Task<List<JobApplicationReadDto>> GetByFirmAppliedAsync(int firmId);
        Task<List<JobApplicationReadDto>> GetByFirmAcceptedAsync(int firmId);
        Task<List<JobApplicationReadDto>> GetByFirmWorkingAsync(int firmId);
        Task<List<JobApplicationReadDto>> GetByFirmFinishedAsync(int firmId);
        Task ApproveAsync(int jobApplicationID);
        Task RejectAsync(int jobApplicationID);
    }
}
