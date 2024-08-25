using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MonitoringDbContext : DbContext
    {
        public MonitoringDbContext(DbContextOptions<MonitoringDbContext> options) : base(options)
        {
        }
        public DbSet<TemperatureData> TemperatureData { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DispenserData> DispenserData { get; set; }
        public DbSet<DispenserLevelData> DispenserLevelData { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemperatureData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Temperature)
                      .HasColumnType("decimal(5, 2)");

            });

            modelBuilder.Entity<Device>()
                .HasOne(d => d.DeviceType)
                .WithMany(dt => dt.Devices)
                .HasForeignKey(d => d.DeviceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TemperatureData>()
                .HasOne(td => td.Sensor)
                .WithMany(s => s.TemperatureDataList)
                .HasForeignKey(td => td.SensorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DispenserData>()
                .HasOne(td => td.Sensor)
                .WithMany(s => s.DispenserDataList)
                .HasForeignKey(td => td.SensorId);

            modelBuilder.Entity<DispenserLevelData>()
                .HasOne(td => td.Sensor)
                .WithMany(s => s.DispenserLevelDataList)
                .HasForeignKey(td => td.SensorId);

            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Board)
                .WithMany(b => b.Sensors)
                .HasForeignKey(s => s.BoardId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.SensorType)
                .WithMany(st => st.Sensors)
                .HasForeignKey(s => s.SensorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Board>()
                .HasOne(b => b.Device)
                .WithMany(d => d.Boards)
                .HasForeignKey(b => b.DeviceId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
