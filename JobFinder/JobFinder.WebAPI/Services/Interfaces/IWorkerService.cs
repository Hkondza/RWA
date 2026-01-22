using JobFinder.WebAPI.DTOs.Worker;
using JobFinder.WebAPI.DTOs.Worker;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IWorkerService
    {
        Task<WorkerReadDto> CreateAsync(WorkerCreateDto dto);
        Task<List<WorkerReadDto>> GetByWorkingAsync(int jobOfferId);
        Task<List<WorkerReadDto>> GetByFinishedAsync(int jobOfferId);

        Task FinishAsync(int jobApplicationID);

    }
}
