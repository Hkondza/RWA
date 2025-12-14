using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.WebAPI.Models
{
    [Table("FirmLocation")]
    public class FirmLocation
    {
        public int FirmID { get; set; }
        public int LocationID { get; set; }
    }
}
