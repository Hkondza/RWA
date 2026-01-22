using BLL.Repositories.Interfaces;
using BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Data;

namespace BLL.Services
{
    public class UserFirmService : IUserFirmService
    {
        private readonly IUserFirmRepository _userFirmRepo;
        private readonly JobFinderContext _context;

        public UserFirmService(
            IUserFirmRepository userFirmRepo,
            JobFinderContext context)
        {
            _userFirmRepo = userFirmRepo;
            _context = context;
        }

        
        public async Task CreateRequestAsync(int userId, int firmId)
        {
            var existing = await _userFirmRepo.GetPendingByUserAsync(userId);
            if (existing != null)
                throw new Exception("Već postoji pending zahtjev.");

            var userFirm = new UserFirm
            {
                UserId = userId,
                FirmId = firmId,
                Status = "Pending",
                RequestedAt = DateTime.Now
            };

            await _userFirmRepo.CreateAsync(userFirm);
        }

        
        public async Task<List<UserFirm>> GetPendingAsync()
        {
            return await _userFirmRepo.GetPendingAsync();
        }

        
        public async Task ApproveAsync(int userFirmId)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var userFirm = await _userFirmRepo.GetByIdAsync(userFirmId)
                ?? throw new Exception("Zahtjev ne postoji.");

            if (userFirm.Status != "Pending")
                throw new Exception("Zahtjev već obrađen.");

            
            userFirm.Status = "Approved";
            userFirm.ApprovedAt = DateTime.Now;

            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Iduser == userFirm.UserId)
                ?? throw new Exception("User ne postoji.");

            user.FirmId = userFirm.FirmId;

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }

        
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
