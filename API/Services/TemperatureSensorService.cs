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

        public async Task AddSensorDataAsync(TemperatureData sensorData)
        {
            if (sensorData == null)
            {
                throw new ArgumentNullException(nameof(sensorData));
            }

            _context.TemperatureData.Add(sensorData);
            await _context.SaveChangesAsync();
        }

        public async Task<TemperatureData> GetSensorDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
