using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly MonitoringDbContext _context;

        public DeviceRepository(MonitoringDbContext context)
        {
            _context = context;
        }
        public async Task<List<DeviceDto>> GetDevicesAsync()
        {
            var devices = _context.Devices
                .Include(d => d.DeviceType)
                .Include(d => d.Boards)
                .ThenInclude(b => b.Sensors)
                .Select(d => new DeviceDto
                {
                    DeviceId = d.DeviceId,
                    DeviceTypeId = d.DeviceTypeId,
                    Description = d.Description,
                    Location = d.Location,
                    Added_at = d.Added_at,
                    DeviceType = d.DeviceType.Type,
                    Boards = d.Boards.Select(b => new BoardDto
                    {
                        BoardId = b.BoardId,
                        BoardSerial = b.BoardSerial,
                        Description = b.Description,
                        IsInstalled = b.IsInstalled,
                        Microcontroller = b.Microcontroller,
                        Sensors = b.Sensors.Select(s => new SensorDto
                        {
                            Description = s.Description,
                            IsAvailable = s.IsAvailable,
                            SensorAddress = s.SensorAddress,
                            SensorId = s.SensorId,
                            SensorName = s.SensorName,
                            SensorTypeId = s.SensorTypeId,

                        }).ToList()
                    }).ToList()
                });
            return await devices.ToListAsync();

    //        var devices =
    //from d in _context.Devices
    //join dt in _context.DeviceTypes on d.DeviceTypeId equals dt.DeviceTypeId
    //join b in _context.Boards on d.DeviceId equals b.DeviceId into boards
    //from b in boards.DefaultIfEmpty()
    //join s in _context.Sensors on b.BoardId equals s.BoardId into sensors
    //from s in sensors.DefaultIfEmpty()
    //select new DeviceDto
    //{
    //    DeviceId = d.DeviceId,
    //    DeviceTypeId = d.DeviceTypeId,
    //    Description = d.Description,
    //    Location = d.Location,
    //    Added_at = d.Added_at,
    //    DeviceType = dt.Type,
    //    Boards = (
    //        from b2 in boards
    //        select new BoardDto
    //        {
    //            BoardId = b2.BoardId,
    //            BoardName = b2.Name,
    //            Sensors = (
    //                from s2 in sensors
    //                where s2.BoardId == b2.BoardId
    //                select new SensorDto
    //                {
    //                    SensorId = s2.SensorId,
    //                    SensorName = s2.Name,
    //                    SensorType = s2.Type,
    //                    SensorValue = s2.Value
    //                }
    //            ).ToList()
    //        }
    //    ).ToList()
    //};

    //        var deviceList = devices.ToList();

        }

        public async Task<int> DeleteDeviceByIdAsync(int deviceId)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var device = await _context.Devices
                    .Include(d => d.Boards)
                    .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

                if (device == null)
                {
                    throw new Exception("Device not found in DataBase");
                }

                var boards = device.Boards;

                if (boards != null)
                {
                   foreach(var board in boards)
                    {

                        board.IsInstalled = false;

                    foreach (var sensor in _context.Sensors.Where(s => s.BoardId == board.BoardId))
                    {
                        sensor.IsAvailable = true;
                    }
                    }
                }

                _context.Devices.Remove(device);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return deviceId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> AddDeviceTypeAsync(DeviceType deviceType)
        {
            if (deviceType == null)
                throw new ArgumentNullException(nameof(deviceType));

            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();

            return deviceType.DeviceTypeId;
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
        public async Task DeleteDeviceTypeById(int deviceTypeId)
        {
            var deviceType = new DeviceType { DeviceTypeId = deviceTypeId };
            _context.DeviceTypes.Attach(deviceType);
            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device.DeviceId;
        }

    }
}
