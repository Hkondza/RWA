using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("Request")]
    public class Request
    {
        [Key]
        public int IDRequest { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int FirmID { get; set; }

        [Required]
        public int LocationID { get; set; }   // ⬅⬅ NOVO

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Created";

        public DateTime CreatedAt { get; set; }
    }
}
