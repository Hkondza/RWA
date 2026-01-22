namespace BLL.DTOs.Firm
{
    public class FirmReadDto
    {
        public int IDFirm { get; set; }
        public string FirmName { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }
        public int JobTypeID { get; set; }
    }
}
