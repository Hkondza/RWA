using JobFinder.WebAPI.Data;
using JobFinder.WebAPI.Models;

namespace JobFinder.WebAPI.Helpers
{
    public static class LogHelper
    {
        public static async Task WriteAsync(
            JobFinderDbContext context,
            string level,
            string message)
        {
            var log = new Log
            {
                Timestamp = DateTime.Now,
                Level = level,
                Message = message
            };

            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}
