using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.DTOs.Profile;
using JobFinder.WebAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Services
{
    public class ProfileService : Interfaces.IProfileService
    {
        private readonly JobFinderDbContext _context;

        public ProfileService(JobFinderDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileReadDto> GetMeAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Firm)
                .FirstOrDefaultAsync(u => u.IDUser == userId)
                ?? throw new Exception("User ne postoji.");

            var pending = await _context.UserFirms
                .Where(x => x.UserID == userId)
                .OrderByDescending(x => x.RequestedAt)
                .FirstOrDefaultAsync();

            return new ProfileReadDto
            {
                IDUser = user.IDUser,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                FirmID = user.FirmID,
                FirmName = user.Firm?.FirmName,
                HasPendingFirmRequest = pending != null && pending.Status == "Pending",
                PendingStatus = pending?.Status
            };
        }

        public async Task UpdateAsync(int userId, ProfileUpdateDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IDUser == userId)
                ?? throw new Exception("User ne postoji.");

            // ✅ update samo normalnih polja
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Phone = dto.Phone;

            await _context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IDUser == userId)
                ?? throw new Exception("User ne postoji.");

            if (!PasswordHelper.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                throw new Exception("Trenutna lozinka nije ispravna.");

            user.PasswordHash = PasswordHelper.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();
        }

        public async Task RequestFirmAsync(int userId, FirmRequestDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IDUser == userId)
                ?? throw new Exception("User ne postoji.");

            // ✅ ako već ima firmu, ne smije slati zahtjev
            if (user.FirmID != null)
                throw new Exception("User već ima firmu dodijeljenu.");

            // ✅ ako već ima pending, ne smije novi
            var existingPending = await _context.UserFirms
                .FirstOrDefaultAsync(x => x.UserID == userId && x.Status == "Pending");

            if (existingPending != null)
                throw new Exception("Već postoji pending zahtjev.");

            int firmId;

            // 1) ako odabrao postojeću firmu
            if (dto.FirmID.HasValue)
            {
                var firmExists = await _context.Firms.AnyAsync(f => f.IDFirm == dto.FirmID.Value);
                if (!firmExists)
                    throw new Exception("Firma ne postoji.");

                firmId = dto.FirmID.Value;
            }
            else
            {
                // 2) ako želi novu firmu
                if (string.IsNullOrWhiteSpace(dto.NewFirmName))
                    throw new Exception("Moraš odabrati firmu ili upisati naziv nove firme.");

                var firm = new Models.Firm
                {
                    FirmName = dto.NewFirmName.Trim()
                };

                _context.Firms.Add(firm);
                await _context.SaveChangesAsync();

                firmId = firm.IDFirm;
            }

            // ✅ kreiraj UserFirm request (Pending)
            var req = new Models.UserFirm
            {
                UserID = userId,
                FirmID = firmId,
                Status = "Pending",
                RequestedAt = DateTime.Now
            };

            _context.UserFirms.Add(req);
            await _context.SaveChangesAsync();
        }
    }
}
