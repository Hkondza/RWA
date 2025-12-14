using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobFinder.WebAPI.Models
{
    
    [Table("Firm")]
    public class Firm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int IDFirm { get; set; }

        [Required]
        [StringLength(100)]
        public string FirmName { get; set; }

        public string? Description { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }

        [ForeignKey(nameof(JobType))]
        public int JobTypeID { get; set; }
    }
}
