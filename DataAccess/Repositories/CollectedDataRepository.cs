using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CollectedDataRepository : ICollectedDataRepository
    {
        private readonly MonitoringDbContext _context;

        public async Task CreateBoardAsync(Board board)
        {
            await _context.Boards.AddAsync(board);
        }
        public CollectedDataRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task<TemperatureData> SaveTemperatureSensorDataAsync(TemperatureData temperatureData)
        {
            _context.TemperatureData.Add(temperatureData);
            await _context.SaveChangesAsync();
            return temperatureData;
        }

        public async Task<Device> GetTemperatureDataByDeviceIdAsync(int deviceId)
        {
            var device = await _context.Devices
                .Include(d => d.Boards)
                    .ThenInclude(b => b.Sensors)
                .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

            var boardsTemp = new List<Board>();

            foreach (var board in device.Boards)
            {
                var sensorsTemp = new List<Sensor>();
                foreach (var sensor in board.Sensors)
                {
                    var completeSensor = await GetTemperatureDataBySensorIdAsync(sensor.SensorId);
                    sensorsTemp.Add(completeSensor);
                }
                board.Sensors = sensorsTemp;
                boardsTemp.Add(board);
            }
            device.Boards = boardsTemp;

            return device ?? throw new Exception("Device not found");
        }

        public async Task<Sensor> GetTemperatureDataBySensorIdAsync(int sensorId)
        {
            var sensor = await _context.Sensors
                .Include(s => s.TemperatureDataList)
                .FirstOrDefaultAsync(s => s.SensorId == sensorId);
            return sensor ?? throw new Exception("Sensor not found");
        }
        public async Task<List<TemperatureData>> GetAllTemperatureMeasurementsAsync()
        {
            return await _context.TemperatureData
                .Include(t => t.Sensor)
                .ToListAsync();
        }

    }
}
