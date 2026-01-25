namespace JobFinder.WebApp.ViewModels.Application
{
    public class JobApplicationDetailsVM
    {

        public int IDJobApplication { get; set; }
        public int JobOfferID { get; set; }

        public string FirmName { get; set; }

        public string LocationName { get; set; }

        public string Message { get; set; }

        public string Title   { get; set; }

        public string Description { get; set; }

        public string Salary { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string JobName { get; set; }
        public DateTime AppliedAt { get; set; }

    }
}
