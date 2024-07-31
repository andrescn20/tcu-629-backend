using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MonitoringDbContext : DbContext
    {
        public MonitoringDbContext(DbContextOptions<MonitoringDbContext> options) : base(options)
        {
        }
        public DbSet<SensorData> SensorData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Specify the precision and scale for decimal properties
                entity.Property(e => e.Temperature)
                      .HasColumnType("decimal(5, 2)");
            });
        }

    }
}
