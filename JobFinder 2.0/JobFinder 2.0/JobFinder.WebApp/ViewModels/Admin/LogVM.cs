namespace JobFinder.WebApp.ViewModels.Admin
{
    public class LogVM
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Level { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
