using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("JobApplication")]
    public class JobApplication
    {
        [Key]
        public int IDJobApplication { get; set; }

        [Required]
        public int JobOfferID { get; set; }

        [Required]
        public int UserID { get; set; }

        public string? Message { get; set; }

        [Required]
        public string Status { get; set; } = "Applied";

        public DateTime AppliedAt { get; set; }
    }
}
