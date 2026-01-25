namespace JobFinder.WebApp.ViewModels.Application
{
    public class JobApplicationUsers
    {
        public int IdJobApplication { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime AppliedAt { get; set; }

    }
}
