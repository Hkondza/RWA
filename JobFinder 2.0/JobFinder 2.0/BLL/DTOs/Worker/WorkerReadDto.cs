namespace BLL.DTOs.Worker
{
    public class WorkerReadDto
    {
        public int IDWorker { get; set; }

        public int JobApplicationId { get; set; }

        public string Status { get; set; }

        public DateTime WorkStartedAt { get; set; }
        public DateTime? WorkFinishedAt { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }



    }
}
