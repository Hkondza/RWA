using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("JobOffer")]
    public class JobOffer
    {
        [Key]
        public int IDJobOffer { get; set; }

        [Required]
        public int FirmID { get; set; }
        public Firm Firm { get; set; }   // ✅ DODANO

        [Required]
        public int JobTypeID { get; set; }
        public JobType JobType { get; set; } // ✅ DODANO

        [Required]
        public int LocationID { get; set; }
        public Location Location { get; set; } // ✅ DODANO

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Salary { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
