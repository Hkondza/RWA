namespace JobFinder.WebAPI.DTOs.JobOffer
{
    public class JobOfferReadDto
    {
        public int IDJobOffer { get; set; }

        public int FirmID { get; set; }
        public int JobTypeID { get; set; }
        public int LocationID { get; set; }

        // ✅ IMENA (za UI)
        public string FirmName { get; set; }
        public string JobName { get; set; }
        public string LocationName { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string? Salary { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
