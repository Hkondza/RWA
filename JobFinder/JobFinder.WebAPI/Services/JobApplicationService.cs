using AutoMapper;
using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.DTOs.JobApplication;
using JobFinder.WebAPI.Helpers;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private const string APPLIED = "Applied";
        private readonly IJobApplicationRepository _repo;
        private readonly IMapper _mapper;
        private readonly JobFinderDbContext _context;

        public JobApplicationService(
            IJobApplicationRepository repo,
            IMapper mapper,JobFinderDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<JobApplicationReadDto> ApplyAsync(JobApplicationCreateDto dto)
        {
            
            if (await _repo.ExistsAsync(dto.JobOfferID, dto.UserID))
            {
                await LogHelper.WriteAsync(
                    _context,
                    "ERROR",
                    $"JobApplication failed. User {dto.UserID} already applied to JobOffer {dto.JobOfferID}"
                );

                throw new Exception("Korisnik se već prijavio na ovaj oglas.");
            }

            var entity = _mapper.Map<JobApplication>(dto);
            entity.Status = APPLIED;
            entity.AppliedAt = DateTime.Now;

            var created = await _repo.CreateAsync(entity);
            await LogHelper.WriteAsync(
                _context,
                "INFO", 
                $"JobApplication created. ID={created.IDJobApplication}, User={created.UserID}, JobOffer={created.JobOfferID}");

            return _mapper.Map<JobApplicationReadDto>(created);
        }

        public async Task ApproveAsync(int jobApplicationID)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var jobApplications = await _repo.GetByApplicationAsync(jobApplicationID)
                ?? throw new Exception("Zahtjev ne postoji.");

            var jobapplication = jobApplications.FirstOrDefault();

            if (jobapplication == null)
            {
                throw new Exception("JobApplication nepostiji");
            }


            if (jobapplication.Status != APPLIED)
                throw new Exception("Zahtjev već obrađen.");

          
            jobapplication.Status = "Approved";
           // dodat approved at u tablicu job application jobapplication.ApprovedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }

        public async Task<List<JobApplicationReadDto>> GetByApplicationAsync(int jobApplicationId)
        {
            var list = await _repo.GetByApplicationAsync(jobApplicationId);
            return _mapper.Map<List<JobApplicationReadDto>>(list);
        }

        public async Task<List<JobApplicationReadDto>> GetByFirmAcceptedAsync(int firmId)
        {
            var list = await _repo.GetByFirmAsync(firmId);

            List<JobApplication> acceptedList = list
                .Where(l => l.Status == "Accepted")
                .ToList();

            return _mapper.Map<List<JobApplicationReadDto>>(acceptedList);
        }

        public async Task<List<JobApplicationReadDto>> GetByFirmAppliedAsync(int firmId)
        {
            var list = await _repo.GetByFirmAsync(firmId);

            List<JobApplication> acceptedList = list
                .Where(l => l.Status == APPLIED)
                .ToList();

            return _mapper.Map<List<JobApplicationReadDto>>(acceptedList);
        }

        public async Task<List<JobApplicationReadDto>> GetByFirmAsync(int firmId)
        {
            var list = await _repo.GetByFirmAsync(firmId);
            return _mapper.Map<List<JobApplicationReadDto>>(list);
        }

        public async Task<List<JobApplicationReadDto>> GetByFirmFinishedAsync(int firmId)
        {
            var list = await _repo.GetByFirmAsync(firmId);

            List<JobApplication> finishedList = list
                .Where(l => l.Status == "Finished")
                .ToList();

            return _mapper.Map<List<JobApplicationReadDto>>(finishedList);
        }

        public async Task<List<JobApplicationReadDto>> GetByFirmWorkingAsync(int firmId)
        {
            var list = await _repo.GetByFirmAsync(firmId);

            List<JobApplication> workingList = list
                .Where(l => l.Status == "Working")
                .ToList();

            return _mapper.Map<List<JobApplicationReadDto>>(workingList);
        }

        public async Task<List<JobApplicationReadDto>> GetByOfferAppliedAsync(int jobOfferId)
        {
            var list = await _repo.GetByOfferAsync(jobOfferId);

            List<JobApplication> appliedList = list 
                .Where(l => l.Status == APPLIED)
                .ToList();
            return _mapper.Map<List<JobApplicationReadDto>>(appliedList);
        }

        public async Task<List<JobApplicationReadDto>> GetByOfferAsync(int jobOfferId)
        {
            var list = await _repo.GetByOfferAsync(jobOfferId);
            return _mapper.Map<List<JobApplicationReadDto>>(list);
        }

        public async Task<List<JobApplicationReadDto>> GetByUserAsync(int userId)
        {
            var list = await _repo.GetByUserAsync(userId);
            return _mapper.Map<List<JobApplicationReadDto>>(list);
        }

        public async Task RejectAsync(int jobApplicationID)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var jobApplications = await _repo.GetByApplicationAsync(jobApplicationID)
                ?? throw new Exception("Zahtjev ne postoji.");

            var jobapplication = jobApplications.FirstOrDefault();

            if (jobapplication == null)
            {
                throw new Exception("JobApplication nepostiji");
            }


            if (jobapplication.Status != APPLIED)
                throw new Exception("Zahtjev već obrađen.");


            jobapplication.Status = "Rejected";
            // dodat approved at u tablicu job application jobapplication.ApprovedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
    }
}
