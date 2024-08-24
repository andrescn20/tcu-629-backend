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
                .Select(d => new DeviceDto
                {
                    DeviceId = d.DeviceId,
                    DeviceTypeId = d.DeviceTypeId,
                    Description = d.Description,
                    Location = d.Location,
                    Added_at = d.Added_at,
                    DeviceType = d.DeviceType.Type,
                });
            return await devices.ToListAsync();
        }

        public async Task<int> DeleteDeviceByIdAsync(int deviceId)
        {
            var device = new Device { DeviceId = deviceId };
            _context.Devices.Attach(device);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return deviceId;
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
