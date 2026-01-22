using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{

    [Table("Workers")]
    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDWorker { get; set; }

        [ForeignKey(nameof(JobApplicationId))]
        public int JobApplicationId { get; set; }
        [Required]
        public JobApplication JobApplication { get; set; }

        public string Status { get; set; } // Working | Finished

        public DateTime WorkStartedAt { get; set; }
        public DateTime? WorkFinishedAt { get; set; }

    }
}
