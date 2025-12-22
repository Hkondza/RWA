using System.ComponentModel.DataAnnotations;

namespace JobFinder.WebApp.ViewModels.Auth
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [RegularExpression("Employer|Employee",
            ErrorMessage = "Odaberite Employer ili Employee.")]
        public string UserType { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
