using JobFinder.WebAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.DTOs.Firm
{
    public class FirmCreateDto
    {
        [Required]
        [StringLength(100)]
        public string FirmName { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }

        [Required]
        public int JobTypeID { get; set; }
    }
}
