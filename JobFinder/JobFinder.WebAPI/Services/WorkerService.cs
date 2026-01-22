using AutoMapper;
using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.DTOs.Worker;
using JobFinder.WebAPI.Helpers;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;

namespace JobFinder.WebAPI.Services
{
    public class WorkerService : IWorkerService
    {
        private const string WORKING = "Working";
        private const string FINISHED = "Finished";
        private readonly IWorkerRepository _repo;
        private readonly IMapper _mapper;
        private readonly JobFinderDbContext _context;

        public WorkerService(IWorkerRepository repo, IMapper mapper, JobFinderDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<WorkerReadDto> CreateAsync(WorkerCreateDto dto)
        {
            if (!await _repo.ExistsAsync(dto.JobApplicationId))
            {
                await LogHelper.WriteAsync(
                    _context,
                    "ERROR",
                    $"Start Work failed. JobApplication: {dto.JobApplicationId} doesn't exist"
                );

                throw new Exception("JobApplication nepostoji !!!");
            }

            var entity = _mapper.Map<Worker>(dto);
            entity.Status = WORKING;
            entity.WorkStartedAt = DateTime.Now;

            //stavit usera ako se sitis

            var created = await _repo.CreateAsync(entity);
            await LogHelper.WriteAsync(
                _context,
                "INFO",
                $"Work started for JobApplication. ID={created.JobApplicationId}");

            return _mapper.Map<WorkerReadDto>(created);
        }

        public async Task FinishAsync(int jobApplicationID)
        {

            using var tx = await _context.Database.BeginTransactionAsync();

            var worker = await _repo.GetByApplicationIdAsync(jobApplicationID)
                ?? throw new Exception("Worker nepostoji.");

           

            if (worker.Status != WORKING)
                throw new Exception("Zahtjev već obrađen.");


            worker.Status = FINISHED;
            worker.WorkFinishedAt = DateTime.Now;
            

            await _context.SaveChangesAsync();
            await tx.CommitAsync();



        }

        public async Task<List<WorkerReadDto>> GetByFinishedAsync(int jobOfferId)
        {
           var list = await _repo.GetAllByJobOfferAsync(jobOfferId);

           return _mapper.Map<List<WorkerReadDto>>(list.Where(l => l.Status == FINISHED).ToList());
        }

        public async Task<List<WorkerReadDto>> GetByWorkingAsync(int jobOfferId)
        {
            var list = await _repo.GetAllByJobOfferAsync(jobOfferId);

            return _mapper.Map<List<WorkerReadDto>>(list.Where(l => l.Status == WORKING).ToList());
        }
    }
}
