namespace JobFinder.WebApp.ViewModels.Admin
{
    public class UserFirmAdminVM
    {
        public int IDUserFirm { get; set; }

        public int UserID { get; set; }
        public string Username { get; set; }

        public int FirmID { get; set; }
        public string FirmName { get; set; }

        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
