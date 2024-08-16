using API.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DataAccess.DTOs.Devices;
using System.Reflection.Metadata.Ecma335;

namespace API.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly MonitoringDbContext _context;

        public DeviceService(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task AddDeviceType(DeviceType deviceType)
        {
            if (deviceType == null)
            {
                throw new ArgumentNullException(nameof(deviceType));
            }

            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices)
        {
            var deviceTypes = new List<DeviceTypeDto>();
            if (withDevices)
            {

                deviceTypes = await _context.DeviceTypes
                      .Select(dt => new DeviceTypeDto
                      {
                          DeviceTypeId = dt.DeviceTypeId,
                          Type = dt.Type,
                          Devices = dt.Devices.Select(d => new DeviceDto
                          {
                              DeviceId = d.DeviceId,
                              DeviceTypeId = d.DeviceTypeId,
                              Description = d.Description,
                              Location = d.Location,
                              Added_at = d.Added_at
                          }).ToList()
                      }
                      ).ToListAsync();

            }

            deviceTypes = await _context.DeviceTypes
                  .Select(dt => new DeviceTypeDto
                  {
                      DeviceTypeId = dt.DeviceTypeId,
                      Type = dt.Type,
                      Devices = null
                  }
                  ).ToListAsync();
            return deviceTypes;
        }

    }

}
