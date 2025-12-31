namespace JobFinder.WebAPI.DTOs.UserFirm
{
    public class UserFirmReadDto
    {
        public int IDUserFirm { get; set; }

        public int UserID { get; set; }
        public string Username { get; set; }

        public int FirmID { get; set; }
        public string FirmName { get; set; }

        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
