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
        public DbSet<FirmLocation> FirmLocations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FirmLocation>()
                .HasKey(fl => new { fl.FirmID, fl.LocationID });
        }

    }
}
