using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.WebApp.ViewModels.JobOffer
{
    public class JobOfferCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Salary { get; set; }

        // Dropdown postojeći
        public int? FirmID { get; set; }
        public int? JobTypeID { get; set; }
        public int? LocationID { get; set; }

        // Ako korisnik upisuje novo
        public string? NewFirmName { get; set; }
        public string? NewJobTypeName { get; set; }
        public string? NewLocationName { get; set; }

        // Liste za dropdown
        public List<SelectListItem> Firms { get; set; } = new();
        public List<SelectListItem> JobTypes { get; set; } = new();
        public List<SelectListItem> Locations { get; set; } = new();
    }
}
