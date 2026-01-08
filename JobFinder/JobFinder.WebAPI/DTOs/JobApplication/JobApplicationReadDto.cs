namespace JobFinder.WebAPI.DTOs.JobApplication
{
    public class JobApplicationReadDto
    {
        public int IDJobApplication { get; set; }
        public int JobOfferID { get; set; }
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }


        public string? Salary { get; set; }

        public string? LocationName { get; set; }

        public bool IsActive { get; set; }

        public string? Message { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
        public string FirmName { get; set; }
        public string JobName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
