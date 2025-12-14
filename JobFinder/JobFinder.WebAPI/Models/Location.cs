using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int IDLocation { get; set; }

        [Required]
        [StringLength(100)]
        public string LocationName { get; set; }
    }
}
