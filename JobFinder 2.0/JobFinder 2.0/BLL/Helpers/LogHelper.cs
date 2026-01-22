using DAL.Data;
using DAL.Models;

namespace BLL.Helpers
{
    public static class LogHelper
    {
        public static async Task WriteAsync(
            JobFinderContext context,
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
