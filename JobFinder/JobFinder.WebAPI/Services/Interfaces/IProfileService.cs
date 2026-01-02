using JobFinder.WebAPI.DTOs.Profile;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileReadDto> GetMeAsync(int userId);
        Task UpdateAsync(int userId, ProfileUpdateDto dto);
        Task ChangePasswordAsync(int userId, ChangePasswordDto dto);
        Task RequestFirmAsync(int userId, FirmRequestDto dto);
    }
}
