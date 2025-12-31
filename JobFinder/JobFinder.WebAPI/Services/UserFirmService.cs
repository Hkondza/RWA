using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Services
{
    public class UserFirmService : IUserFirmService
    {
        private readonly IUserFirmRepository _userFirmRepo;
        private readonly JobFinderDbContext _context;

        public UserFirmService(
            IUserFirmRepository userFirmRepo,
            JobFinderDbContext context)
        {
            _userFirmRepo = userFirmRepo;
            _context = context;
        }

        // Employer šalje zahtjev
        public async Task CreateRequestAsync(int userId, int firmId)
        {
            var existing = await _userFirmRepo.GetPendingByUserAsync(userId);
            if (existing != null)
                throw new Exception("Već postoji pending zahtjev.");

            var userFirm = new UserFirm
            {
                UserID = userId,
                FirmID = firmId,
                Status = "Pending",
                RequestedAt = DateTime.Now
            };

            await _userFirmRepo.CreateAsync(userFirm);
        }

        // Admin vidi sve pending zahtjeve
        public async Task<List<UserFirm>> GetPendingAsync()
        {
            return await _userFirmRepo.GetPendingAsync();
        }

        // Admin odobrava
        public async Task ApproveAsync(int userFirmId)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var userFirm = await _userFirmRepo.GetByIdAsync(userFirmId)
                ?? throw new Exception("Zahtjev ne postoji.");

            if (userFirm.Status != "Pending")
                throw new Exception("Zahtjev već obrađen.");

            // 1️⃣ update UserFirm
            userFirm.Status = "Approved";
            userFirm.ApprovedAt = DateTime.Now;

            // 2️⃣ upis u Users
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IDUser == userFirm.UserID)
                ?? throw new Exception("User ne postoji.");

            user.FirmID = userFirm.FirmID;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }

        // Admin odbija
        public async Task RejectAsync(int userFirmId)
        {
            var userFirm = await _userFirmRepo.GetByIdAsync(userFirmId)
                ?? throw new Exception("Zahtjev ne postoji.");

            if (userFirm.Status != "Pending")
                throw new Exception("Zahtjev već obrađen.");

            userFirm.Status = "Rejected";
            await _userFirmRepo.SaveChangesAsync();
        }
    }
}
