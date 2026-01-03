using AutoMapper;
using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.DTOs.JobOffer;
using JobFinder.WebAPI.Helpers;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;

namespace JobFinder.WebAPI.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly IJobOfferRepository _repo;
        private readonly IMapper _mapper;
        private readonly JobFinderDbContext _context;

        public JobOfferService(IJobOfferRepository repo, IMapper mapper, JobFinderDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
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
            // 1️⃣ Resolve Firm
            int firmId;
            if (dto.FirmID.HasValue)
            {
                firmId = dto.FirmID.Value;
            }
            else
            {
                //radi urednosti strancie stavio sam ovo default
                //moga sam dto napunit sa svim podatima i onda jos napraviti text fildove
                //al ovako je bolje
                var firm = new Firm
                {
                    FirmName = "test", //ovo tribas pomaknit ili skontat
                    Email = "email@gmail.com",
                    PhoneNumber = "0994362136",
                    Description = dto.Description,
                    WebsiteUrl = "https://www.elektroplus.hr",
                    JobTypeID = dto.JobTypeID.GetValueOrDefault(1) 
                    
                };
                _context.Firms.Add(firm);
                await _context.SaveChangesAsync();
                firmId = firm.IDFirm;
            }

            // 2️⃣ Resolve JobType
            int jobTypeId;
            if (dto.JobTypeID.HasValue)
            {
                jobTypeId = dto.JobTypeID.Value;
            }
            else
            {
                var jobType = new JobType
                {
                    JobName = dto.NewJobTypeName
                };
                _context.JobTypes.Add(jobType);
                await _context.SaveChangesAsync();
                jobTypeId = jobType.IDJobType;
            }

            // 3️⃣ Resolve Location
            int locationId;
            if (dto.LocationID.HasValue)
            {
                locationId = dto.LocationID.Value;
            }
            else
            {
                var location = new Location
                {
                    LocationName = dto.NewLocationName
                };
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                locationId = location.IDLocation;
            }

            // 4️⃣ Create JobOffer
            var entity = new JobOffer
            {
                Title = dto.Title,
                Description = dto.Description,
                Salary = dto.Salary,
                FirmID = firmId,
                JobTypeID = jobTypeId,
                LocationID = locationId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var created = await _repo.CreateAsync(entity);

            return _mapper.Map<JobOfferReadDto>(created);
        }

    }
}
