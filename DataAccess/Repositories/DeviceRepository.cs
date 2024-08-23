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

    }
}
