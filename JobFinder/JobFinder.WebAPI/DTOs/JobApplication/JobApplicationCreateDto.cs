using System.ComponentModel.DataAnnotations;

namespace JobFinder.WebAPI.DTOs.JobApplication
{
    public class JobApplicationCreateDto
    {
        [Required]
        public int JobOfferID { get; set; }

        [Required]
        public int UserID { get; set; }

        public string? Message { get; set; }
    }
}
