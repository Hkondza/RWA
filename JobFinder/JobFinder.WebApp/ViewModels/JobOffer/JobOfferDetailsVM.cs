namespace JobFinder.WebApp.ViewModels.JobOffer
{
    public class JobOfferDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FirmName { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
