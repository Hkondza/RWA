using BLL.DTOs.Profile;
using BLL.Helpers;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private const string PENDING = "Pending";
        private readonly JobFinderContext _context;

        public ProfileService(JobFinderContext context)
        {
            _context = context;
        }

        public async Task<ProfileReadDto> GetMeAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Firm)
                .FirstOrDefaultAsync(u => u.Iduser == userId)
                ?? throw new Exception("User ne postoji.");

            var pending = await _context.UserFirms
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.RequestedAt)
                .FirstOrDefaultAsync();

            return new ProfileReadDto
            {
                IDUser = user.Iduser,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                FirmID = user.FirmId,
                FirmName = user.Firm?.FirmName,
                HasPendingFirmRequest = pending != null && pending.Status == PENDING,
                PendingStatus = pending?.Status
            };
        }

        public async Task UpdateAsync(int userId, ProfileUpdateDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Iduser == userId)
                ?? throw new Exception("User ne postoji.");

          
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Phone = dto.Phone;

            await _context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Iduser == userId)
                ?? throw new Exception("User ne postoji.");

            if (!PasswordHelper.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                throw new Exception("Trenutna lozinka nije ispravna.");

            user.PasswordHash = PasswordHelper.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();
        }

        public async Task RequestFirmAsync(int userId, FirmRequestDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Iduser == userId)
                ?? throw new Exception("User ne postoji.");

            if (user.FirmId != null)
                throw new Exception("User već ima firmu dodijeljenu.");

            
            var existingPending = await _context.UserFirms
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Status == PENDING);

            if (existingPending != null)
                throw new Exception("Već postoji pending zahtjev.");

            int firmId;

            
            if (dto.FirmID.HasValue)
            {
                var firmExists = await _context.Firms.AnyAsync(f => f.Idfirm == dto.FirmID.Value);
                if (!firmExists)
                    throw new Exception("Firma ne postoji.");

                firmId = dto.FirmID.Value;
            }
            else
            {
                
                if (string.IsNullOrWhiteSpace(dto.NewFirmName))
                    throw new Exception("Moraš odabrati firmu ili upisati naziv nove firme.");

                //radi urednosti strancie stavio sam ovo default
                //moga sam dto napunit sa svim podatima i onda jos napraviti text fildove
                //al ovako je bolje
                var firm = new Firm
                {
                    FirmName = dto.NewFirmName,
                    Email = "email@gmail.com",
                    PhoneNumber = "0994362136",
                    Description = "Description",
                    WebsiteUrl = "https://www.test.hr",
                    JobTypeId = 1

                };

                _context.Firms.Add(firm);
                await _context.SaveChangesAsync();

                firmId = firm.Idfirm;
            }

            
            var req = new UserFirm
            {
                UserId = userId,
                FirmId = firmId,
                Status = PENDING,
                RequestedAt = DateTime.Now
            };

            _context.UserFirms.Add(req);
            await _context.SaveChangesAsync();
        }
    }
}
