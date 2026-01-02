using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobFinder.WebApp.ViewModels.Profile
{
    public class ProfileVM
    {
        public int IDUser { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }

        public int? FirmID { get; set; }
        public string? FirmName { get; set; }

        public bool HasPendingFirmRequest { get; set; }
        public string? PendingStatus { get; set; }

        public List<SelectListItem> Firms { get; set; } = new();
    }
}
