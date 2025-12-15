namespace JobFinder.WebAPI.DTOs.User
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserReadDto User { get; set; }
    }
}
