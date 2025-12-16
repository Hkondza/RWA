using JobFinder.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.WebAPI.Data
{
    public class JobFinderDbContext : DbContext
    {
        public JobFinderDbContext(DbContextOptions<JobFinderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Firm> Firms { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }


    }
}
