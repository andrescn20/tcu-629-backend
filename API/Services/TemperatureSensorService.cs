using API.Interfaces;
using DataAccess;
using DataAccess.Models;

namespace API.Services
{
    public class TemperatureSensorService : ITemperatureSensorService
    {
        private readonly MonitoringDbContext _context;

        public TemperatureSensorService(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task AddSensorDataAsync(SensorData sensorData)
        {
            if (sensorData == null)
            {
                throw new ArgumentNullException(nameof(sensorData));
            }

            _context.SensorData.Add(sensorData);
            await _context.SaveChangesAsync();
        }

        public async Task<SensorData> GetSensorDataByIdAsync(int id)
        {
            return await _context.SensorData.FindAsync(id);
        }
    }

}
