using BLL.DTOs.Worker;

namespace BLL.Services.Interfaces
{
    public interface IWorkerService
    {
        Task<WorkerReadDto> CreateAsync(WorkerCreateDto dto);
        Task<List<WorkerReadDto>> GetAllByJobOfferAsync(int jobOfferId);
        Task<List<WorkerReadDto>> GetByWorkingAsync(int jobOfferId);
        Task<List<WorkerReadDto>> GetByFinishedAsync(int jobOfferId);
        Task<WorkerReadDto> GetByJobApplicationAsync(int jobApplicationd);

        Task FinishAsync(int jobApplicationID);

    }
}
