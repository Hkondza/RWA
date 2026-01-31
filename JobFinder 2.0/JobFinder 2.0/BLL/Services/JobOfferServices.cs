using AutoMapper;
using BLL.DTOs.Firm;
using BLL.DTOs.JobOffer;
using BLL.Repositories.Interfaces;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Models;

namespace BLL.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly IJobOfferRepository _repo;
        private readonly IMapper _mapper;
        private readonly JobFinderContext _context;

        public JobOfferService(IJobOfferRepository repo, IMapper mapper, JobFinderContext context)
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
            // ovo pomakni stari kod nova logika
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
                    FirmName = "test2", //ovo tribas pomaknit ili skontat
                    Email = "email@gmail.com",
                    PhoneNumber = "0994362136",
                    Description = dto.Description,
                    WebsiteUrl = "https://www.elektroplus.hr",
                    JobTypeId = dto.JobTypeID.GetValueOrDefault(1) 
                    
                };
                _context.Firms.Add(firm);
                await _context.SaveChangesAsync();
                firmId = firm.Idfirm;
            }

            
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
                jobTypeId = jobType.IdjobType;
            }

            
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
                locationId = location.Idlocation;
            }

            
            var entity = new JobOffer
            {
                Title = dto.Title,
                Description = dto.Description,
                Salary = dto.Salary,
                FirmId = firmId,
                JobTypeId = jobTypeId,
                LocationId = locationId,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var created = await _repo.CreateAsync(entity);

            return _mapper.Map<JobOfferReadDto>(created);
        }

        public async Task<List<JobOfferReadDto>> GetByFirmAsync(int firmId)
        {
            var offers = await _repo.GetAllByFirmAsync(firmId);
            return _mapper.Map<List<JobOfferReadDto>>(offers);
        }

        public async Task<List<JobOfferReadDto>> GetAllSearchAsync(string? search, int page, int pageSize)
        {
            var firms = await _repo.GetAllSearchAsync(search, page, pageSize);
            return _mapper.Map<List<JobOfferReadDto>>(firms);
        }

        public async Task<int> CountAsync(string? search)
        {
            return await _repo.CountAsync(search);
        }

        public async Task RemoveJobOffer(int id)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var offer = await _repo.GetByIdAsync(id)
                ?? throw new Exception("JobOffer Ne popsotji.");



            if (!offer.IsActive)
                throw new Exception("Zahtjev već obrađen.");


            offer.IsActive = false;
         
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
    }
}
