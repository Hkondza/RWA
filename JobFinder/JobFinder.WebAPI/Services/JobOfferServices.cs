using AutoMapper;
using JobFinder.WebAPI.DTOs.JobOffer;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;

namespace JobFinder.WebAPI.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly IJobOfferRepository _repo;
        private readonly IMapper _mapper;

        public JobOfferService(IJobOfferRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<JobOfferReadDto>> GetAllAsync()
        {
            var offers = await _repo.GetActiveAsync();
            return _mapper.Map<List<JobOfferReadDto>>(offers);
        }

        public async Task<JobOfferReadDto?> GetByIdAsync(int id)
        {
            var offer = await _repo.GetByIdAsync(id);
            return offer == null ? null : _mapper.Map<JobOfferReadDto>(offer);
        }

        public async Task<JobOfferReadDto> CreateAsync(JobOfferCreateDto dto)
        {
            var entity = _mapper.Map<JobOffer>(dto);
            entity.CreatedAt = DateTime.Now;
            entity.IsActive = true;

            var created = await _repo.CreateAsync(entity);
            return _mapper.Map<JobOfferReadDto>(created);
        }
    }
}
