using AutoMapper;
using JobFinder.WebAPI.DTOs.Firm;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;

namespace JobFinder.WebAPI.Services
{
    public class FirmService : IFirmService
    {
        private readonly IFirmRepository _repo;
        private readonly IMapper _mapper;

        public FirmService(IFirmRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET ALL + SEARCH + PAGING
        public async Task<List<FirmReadDto>> GetAllAsync(string? search, int page, int pageSize)
        {
            var firms = await _repo.GetAllAsync(search, page, pageSize);
            return _mapper.Map<List<FirmReadDto>>(firms);
        }

        public async Task<int> CountAsync(string? search)
        {
            return await _repo.CountAsync(search);
        }

        // GET BY ID
        public async Task<FirmReadDto?> GetByIdAsync(int id)
        {
            var firm = await _repo.GetByIdAsync(id);
            if (firm == null)
                return null;

            return _mapper.Map<FirmReadDto>(firm);
        }

        // CREATE
        public async Task<FirmReadDto> CreateAsync(FirmCreateDto dto)
        {
            var firm = _mapper.Map<Firm>(dto);

            var created = await _repo.CreateAsync(firm);

            return _mapper.Map<FirmReadDto>(created);
        }

        // UPDATE
        public async Task<bool> UpdateAsync(int id, FirmUpdateDto dto)
        {
            var firm = await _repo.GetByIdAsync(id);
            if (firm == null)
                return false;

            _mapper.Map(dto, firm);

            await _repo.UpdateAsync(firm);
            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var firm = await _repo.GetByIdAsync(id);
            if (firm == null)
                return false;

            await _repo.DeleteAsync(firm);
            return true;
        }
    }
}
