using JobFinder.WebAPI.DTOs.User;

namespace JobFinder.WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReadDto> RegisterAsync(UserRegisterDto dto);
        Task<UserReadDto> LoginAsync(UserLoginDto dto);
    }
}
