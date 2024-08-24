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
        public async Task<List<Sensor>> GetSensorsByIdAsync(List<int> sensorIds)
        {
            return await _context.Sensors
                                 .Where(s => sensorIds.Contains(s.SensorId))
                                 .ToListAsync();
        }
        public async Task UpdateSensorsAsync(List<Sensor> sensors)
        {
            _context.Sensors.UpdateRange(sensors);
            await _context.SaveChangesAsync();
        }
        public async Task<Sensor> GetSensorByIdAsync(int sensorId)
        {
            var sensor = await _context.Sensors.FindAsync(sensorId);
            if(sensor == null)
            {
                throw new Exception("Sensor not found");
            }
            return sensor;
        }

        public async Task DeleteSensorByIdAsync(int sensorId)
        {
            var sensor = new Sensor { SensorId = sensorId };
            _context.Sensors.Attach(sensor);
            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();
        }

        public async Task AddSensorAsync(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
        }
        public async Task<int> AddSensorTypeAsync(SensorType sensorType)
        {
            if (sensorType == null)
                throw new ArgumentNullException(nameof(sensorType));

            _context.SensorTypes.Add(sensorType);
            await _context.SaveChangesAsync();

            return sensorType.SensorTypeId;
        }

        public async Task DeleteSensorTypeById(int sensorTypeId)
        {
            var sensorType = new SensorType { SensorTypeId = sensorTypeId };
            _context.SensorTypes.Attach(sensorType);
            _context.SensorTypes.Remove(sensorType);
            await _context.SaveChangesAsync();
        }
        public async Task<List<SensorTypeDto>> GetSensorTypesAsync()
        {
            var sensorTypes = await _context.SensorTypes
                .Select(st => new SensorTypeDto
                {
                    TypeId = st.SensorTypeId,
                    Type = st.Type
                }).ToListAsync();

            return sensorTypes;
        }

        public async Task<int> GetSensorIdByAddressAsync(string sensorAddress)
        {
            var sensor = await _context.Sensors
            .Where(s => s.SensorAddress == sensorAddress)
            .Select(s => s.SensorId)
            .FirstOrDefaultAsync();

            return sensor;
        }

        public async Task<Sensor> GetSensorByAddressAsync(string sensorAddress)
        {
            var sensor = await _context.Sensors
            .Where(s => s.SensorAddress == sensorAddress)
            .FirstOrDefaultAsync();

            return sensor;
        }

        public async Task<List<SensorDto>> GetAllSensors()
        {
            var sensors = await _context.Sensors
            .Include(s => s.SensorType)
            .Select(s => new SensorDto
            {
                SensorId = s.SensorId,
                Description = s.Description,
                IsAvailable = s.IsAvailable,
                SensorName = s.SensorName,
                SensorTypeId = s.SensorTypeId,
                SensorType = s.SensorType.Type,
            }).ToListAsync();

            return sensors;
        }
    }
}
