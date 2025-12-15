namespace JobFinder.WebAPI.DTOs.User
{
    public class UserReadDto
    {
        public int IDUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; }
    }
}
