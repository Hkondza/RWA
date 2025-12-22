using System.ComponentModel.DataAnnotations;

namespace JobFinder.WebAPI.DTOs.User
{
    public class UserRegisterDto
    {
        [Required] public string Username { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }

        [Required]
        [RegularExpression("Employer|Employee")]
        public string UserType{ get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
    }
}
