using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class HardwareRepository : IHardwareRepository
    {
        private readonly MonitoringDbContext _context;

        public HardwareRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddDeviceTypeAsync(DeviceType deviceType)
        {
            if (deviceType == null)
                throw new ArgumentNullException(nameof(deviceType));

            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();

            return deviceType.DeviceTypeId;
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

        public async Task DeleteDeviceTypeById(int deviceTypeId)
        {
            var deviceType = new DeviceType { DeviceTypeId = deviceTypeId };
            _context.DeviceTypes.Attach(deviceType);
            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSensorTypeAsync(SensorType sensorType)
        {
            _context.SensorTypes.Remove(sensorType);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DeviceTypeDto>> GetDeviceTypesAsync(bool withDevices)
        {
            var deviceTypes = _context.DeviceTypes
                .Select(dt => new DeviceTypeDto
                {
                    TypeId = dt.DeviceTypeId,
                    Type = dt.Type,
                    Devices = withDevices ? dt.Devices.Select(d => new DeviceDto
                    {
                        DeviceId = d.DeviceId,
                        DeviceTypeId = d.DeviceTypeId,
                        Description = d.Description,
                        Location = d.Location,
                        Added_at = d.Added_at
                    }).ToList() : null
                });

            return await deviceTypes.ToListAsync();
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

        public async Task<int> AddDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device.DeviceId;
        }

        public async Task<int> AddBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board.BoardId;
        }

        public async Task AddSensorsAsync(List<Sensor> sensors)
        {
            _context.Sensors.AddRange(sensors);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetSensorIdByAddressAsync(string sensorAddress)
        {
            var sensor = await _context.Sensors
            .Where(s => s.SensorAddress == sensorAddress)
            .Select(s => s.SensorId)
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

        public async Task<List<BoardDto>> GetAllBoards()
        {
            var boards = await _context.Boards
            .Select(b => new BoardDto
            {
                BoardId = b.BoardId,
                Description = b.Description,
                IsInstalled = b.IsInstalled,
                Microcontroller = b.BoardSerial,
            }).ToListAsync();


            return boards;
        }

        public async Task<int> GetBoardIdBySerialAsync(string boardSerial)
        {
            var board = await _context.Boards
            .Where(b => b.BoardSerial == boardSerial)
            .Select(b => b.BoardId)
            .FirstOrDefaultAsync();

            return board;
        }
    }
}
