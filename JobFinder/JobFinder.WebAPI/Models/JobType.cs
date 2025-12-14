using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("JobType")]
    public class JobType
    {
        [Key]
        public int IDJobType { get; set; }

        [Required]
        [StringLength(100)]
        public string JobName { get; set; }
    }
}
