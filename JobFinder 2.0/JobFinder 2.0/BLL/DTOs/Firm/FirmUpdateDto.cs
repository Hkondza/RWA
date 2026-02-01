using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Firm
{
    public class FirmUpdateDto
    {


        [Required]
        [StringLength(100)]
        public string? IDFirm { get; set; }

        [Required]
        public int JobTypeID { get; set; }



        public string FirmName { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
    }
}
