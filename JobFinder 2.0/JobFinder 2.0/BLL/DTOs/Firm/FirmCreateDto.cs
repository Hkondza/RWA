using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Firm
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
