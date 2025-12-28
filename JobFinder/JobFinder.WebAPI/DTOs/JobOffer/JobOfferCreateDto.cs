using System.ComponentModel.DataAnnotations;

namespace JobFinder.WebAPI.DTOs.JobOffer
{
    
        public class JobOfferCreateDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string? Salary { get; set; }

            // Firm
            public int? FirmID { get; set; }
            public string? NewFirmName { get; set; }

            // JobType
            public int? JobTypeID { get; set; }
            public string? NewJobTypeName { get; set; }

            // Location
            public int? LocationID { get; set; }
            public string? NewLocationName { get; set; }
        }

    
}
