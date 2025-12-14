using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("Request")]
    public class Request
    {
        [Key]
        public int IDRequest { get; set; }

        public int UserID { get; set; }
        public int FirmID { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
