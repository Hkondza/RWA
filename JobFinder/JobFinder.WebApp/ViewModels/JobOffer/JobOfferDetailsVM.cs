namespace JobFinder.WebApp.ViewModels.JobOffer
{
    public class JobOfferDetailsVM
    {
        public int IDJobOffer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FirmName { get; set; }
        public string LocationName { get; set; }
        public string JobName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
