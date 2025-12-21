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
            // Poslovno pravilo: nema duple prijave
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
            entity.Status = "Applied";
            entity.AppliedAt = DateTime.Now;

            var created = await _repo.CreateAsync(entity);
            await LogHelper.WriteAsync(
                _context,
                "INFO", 
                $"JobApplication created. ID={created.IDJobApplication}, User={created.UserID}, JobOffer={created.JobOfferID}");

            return _mapper.Map<JobApplicationReadDto>(created);
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
    }
}
