namespace JobFinder.WebApp.ViewModels.Application
{
    public class JobApplicationListVM
    {
        public int IDJobOffer { get; set; }
        public string FirmName { get; set; }

        public string Status { get; set; }
        public string JobName { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
