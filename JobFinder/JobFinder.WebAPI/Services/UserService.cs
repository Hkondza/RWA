using AutoMapper;
using JobFinder.WebAPI.DTOs.User;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace JobFinder.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserReadDto> RegisterAsync(UserRegisterDto dto)
        {
            if (await _repo.ExistsAsync(dto.Email, dto.Username))
                throw new Exception("Korisnik već postoji.");

            var user = _mapper.Map<User>(dto);

            // Dummy hash (OK za projekt)
            using var sha = SHA256.Create();
            user.PasswordHash = Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))
            );

            user.Role = "User";

            var created = await _repo.CreateAsync(user);
            return _mapper.Map<UserReadDto>(created);
        }

        public async Task<UserReadDto> LoginAsync(UserLoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Neispravan email ili lozinka.");

            using var sha = SHA256.Create();
            var hash = Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))
            );

            if (user.PasswordHash != hash)
                throw new Exception("Neispravan email ili lozinka.");

            return _mapper.Map<UserReadDto>(user);
        }
    }
}
