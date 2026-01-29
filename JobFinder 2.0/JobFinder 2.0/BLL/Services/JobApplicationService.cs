using AutoMapper;
using BLL.DTOs.JobApplication;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using BLL.Repositories.Interfaces;
using BLL.Helpers;

namespace BLL.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private const string APPLIED = "Applied";
        private const string APPROVED = "Approved";
        private const string REJECTED = "Rejected";

        private readonly IJobApplicationRepository _repo;
        private readonly IMapper _mapper;
        private readonly JobFinderContext _context;

        public JobApplicationService(
            IJobApplicationRepository repo,
            IMapper mapper,JobFinderContext context)
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
                $"JobApplication created. ID={created.IdjobApplication}, User={created.UserId}, JobOffer={created.JobOfferId}");

            return _mapper.Map<JobApplicationReadDto>(created);
        }

        public async Task ApproveAsync(int jobApplicationID)
        {



            // 1. Prvo dohvati podatke (van transakcije)
            var jobApplications = await _repo.GetByApplicationAsync(jobApplicationID);
            var jobapplication = jobApplications?.FirstOrDefault();

            if (jobapplication == null) throw new Exception("Ne postoji.");
            if (jobapplication.Status != APPLIED) throw new Exception("Već obrađen.");

            // 2. Tek sada otvori transakciju za promjenu
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                jobapplication.Status = APPROVED;
                

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }


            // var jobApplications = await _repo.GetByApplicationAsync(jobApplicationID)
            //     ?? throw new Exception("Zahtjev ne postoji.");

            // var jobapplication = jobApplications.FirstOrDefault();

            // using var tx = await _context.Database.BeginTransactionAsync();


            // if (jobapplication == null)
            // {
            //     throw new Exception("JobApplication nepostiji");
            // }


            // if (jobapplication.Status != APPLIED)
            //     throw new Exception("Zahtjev već obrađen.");


            // jobapplication.Status = APPROVED;
            //// dodat approved at u tablicu job application jobapplication.ApprovedAt = DateTime.Now;

            // await _context.SaveChangesAsync();
            // await tx.CommitAsync();
        }


        //job application bezveze vraca listu jedan postoji samo jedan aplplciation sa tim id
        //ali zato postoji vise joboffer sa tim id 
        public async Task<List<JobApplicationReadDto>> GetByApplicationAsync(int jobApplicationId)
        {
            var list = await _repo.GetByApplicationAsync(jobApplicationId);
            return _mapper.Map<List<JobApplicationReadDto>>(list);
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



        //ovoa dva donja pomakni . nov logika ide u tablicu worker
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

        public async Task<List<JobApplicationReadDto>> GetByOfferApprovedAsync(int jobOfferId)
        {
            var list = await _repo.GetByOfferAsync(jobOfferId);

            return _mapper.Map<List<JobApplicationReadDto>>(list.Where(l => l.Status == APPROVED).ToList());
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


            jobapplication.Status = REJECTED;
            // dodat approved at u tablicu job application jobapplication.ApprovedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
    }
}
