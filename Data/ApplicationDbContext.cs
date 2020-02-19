using Microsoft.EntityFrameworkCore;
using tmetricstatistics.Model;

namespace tmetricstatistics.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<WorkingHours> TimeEntries { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AppConfig> AppConfig { get; set; }
    }
}