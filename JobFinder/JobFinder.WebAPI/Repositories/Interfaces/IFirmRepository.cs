using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Repositories.Interfaces
{
    public interface IFirmRepository
    {
        Task<List<Firm>> GetAllAsync(string? search, int page, int pageSize);
        Task<int> CountAsync(string? search);

        Task<Firm?> GetByIdAsync(int id);

        Task<Firm> CreateAsync(Firm firm);

        Task UpdateAsync(Firm firm);

        Task DeleteAsync(Firm firm);
    }
}
