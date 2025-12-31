using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("UserFirm")]
    public class UserFirm
    {
        [Key]
        public int IDUserFirm { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        [Required]
        public int FirmID { get; set; }

        [ForeignKey(nameof(FirmID))]
        public Firm Firm { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Approved, Rejected

        [Required]
        public DateTime RequestedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }
}
