using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CollectedDataRepository : ICollectedDataRepository
    {
        private readonly MonitoringDbContext _context;

        public CollectedDataRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task SaveTemperatureSensorDataAsync(TemperatureData temperatureData)
        {
            _context.TemperatureData.Add(temperatureData);
            await _context.SaveChangesAsync();
        }

    }
}
