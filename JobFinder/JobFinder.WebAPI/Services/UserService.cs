using AutoMapper;
using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.DTOs.User;
using JobFinder.WebAPI.Helpers;
using JobFinder.WebAPI.Models;
using JobFinder.WebAPI.Repositories.Interfaces;
using JobFinder.WebAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JobFinder.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        private readonly IConfiguration _config;
        private readonly JobFinderDbContext _context;

        public UserService(IUserRepository repo, IMapper mapper, IConfiguration config, JobFinderDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
            _context = context;
        }


        public async Task<UserReadDto> RegisterAsync(UserRegisterDto dto)
        {
            if (await _repo.ExistsAsync(dto.Email, dto.Username))
            {
                await LogHelper.WriteAsync(
                    _context,
                    "ERROR",
                    $"User registration failed. Email={dto.Email}, Username={dto.Username}"
                );

                throw new Exception("Korisnik već postoji.");
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = PasswordHelper.HashPassword(dto.Password);
            //ovo ce tribat osigurat nekako !!!
            user.Role = "User";

            var created = await _repo.CreateAsync(user);
            await LogHelper.WriteAsync(
                    _context,
                "INFO",
              $"User registered. ID={created.IDUser}, Email={created.Email}"
                  );
            return _mapper.Map<UserReadDto>(created);
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.IDUser.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    int.Parse(_config["Jwt:ExpiresInMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public async Task<LoginResponseDto> LoginAsync(UserLoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                await LogHelper.WriteAsync(
                    _context,
                    "ERROR",
                    $"Login failed. Email={dto.Email}"
                );

                throw new Exception("Neispravni podaci.");
            }

            using var sha = SHA256.Create();
            var hash = Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))
            );

            if (!PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
            {
                await LogHelper.WriteAsync(
                    _context,
                    "ERROR",
                    $"Login failed (wrong password). UserID={user.IDUser}"
                );

                throw new Exception("Neispravni podaci.");
            }

            var token = GenerateJwtToken(user);

            await LogHelper.WriteAsync(
                _context,
                "INFO",
                $"User logged in. ID={user.IDUser}, Email={user.Email}"
            );

            return new LoginResponseDto
            {
                Token = token,
                User = _mapper.Map<UserReadDto>(user)
            };
        }


    }
}
