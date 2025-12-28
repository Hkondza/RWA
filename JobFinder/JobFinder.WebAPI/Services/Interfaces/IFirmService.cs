using JobFinder.WebAPI.DTOs.Firm;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IFirmService
    {
        Task<List<FirmReadDto>> GetAllSearchAsync(string? search, int page, int pageSize);
        Task<int> CountAsync(string? search);

        Task<FirmReadDto?> GetByIdAsync(int id);

        Task<FirmReadDto> CreateAsync(FirmCreateDto dto);

        Task<bool> UpdateAsync(int id, FirmUpdateDto dto);

        Task<bool> DeleteAsync(int id);

        Task<List<FirmReadDto>>GetAllAsync();
    }
}
