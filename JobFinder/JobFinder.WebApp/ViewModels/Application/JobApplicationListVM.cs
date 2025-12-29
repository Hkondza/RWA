namespace JobFinder.WebApp.ViewModels.Application
{
    public class JobApplicationListVM
    {
        public int JobOfferID { get; set; }
        public string FirmName { get; set; }

        public string Status { get; set; }
        public string JobName { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
