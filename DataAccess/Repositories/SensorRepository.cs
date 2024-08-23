using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly MonitoringDbContext _context;

        public SensorRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task DeleteSensorByIdAsync(int sensorId)
        {
            var sensor = new Sensor { SensorId = sensorId};
            _context.Sensors.Attach(sensor);
            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();
        }

    }
}
