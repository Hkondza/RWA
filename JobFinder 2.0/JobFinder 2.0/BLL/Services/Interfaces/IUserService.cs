using BLL.DTOs.User;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReadDto> RegisterAsync(UserRegisterDto dto);
        Task<LoginResponseDto> LoginAsync(UserLoginDto dto);
        
      }
}
