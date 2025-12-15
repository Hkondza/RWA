namespace JobFinder.WebAPI.DTOs.JobApplication
{
    public class JobApplicationReadDto
    {
        public int IDJobApplication { get; set; }
        public int JobOfferID { get; set; }
        public int UserID { get; set; }

        public string? Message { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
