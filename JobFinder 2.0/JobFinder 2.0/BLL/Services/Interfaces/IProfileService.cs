using BLL.DTOs.Profile;

namespace BLL.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileReadDto> GetMeAsync(int userId);
        Task UpdateAsync(int userId, ProfileUpdateDto dto);
        Task ChangePasswordAsync(int userId, ChangePasswordDto dto);
        Task RequestFirmAsync(int userId, FirmRequestDto dto);
    }
}
