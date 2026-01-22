namespace BLL.DTOs.Profile
{
    public class ProfileReadDto
    {
        public int IDUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }

        public int? FirmID { get; set; }
        public string? FirmName { get; set; }

        public bool HasPendingFirmRequest { get; set; }
        public string? PendingStatus { get; set; } // Pending / Rejected 
    }
}
