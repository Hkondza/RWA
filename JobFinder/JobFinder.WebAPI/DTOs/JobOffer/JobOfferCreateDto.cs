using System.ComponentModel.DataAnnotations;

namespace JobFinder.WebAPI.DTOs.JobOffer
{
    public class JobOfferCreateDto
    {
        [Required] public int FirmID { get; set; }
        [Required] public int JobTypeID { get; set; }
        [Required] public int LocationID { get; set; }

        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        public string? Salary { get; set; }
    }
}
