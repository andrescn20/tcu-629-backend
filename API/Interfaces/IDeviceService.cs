using DataAccess.Models;
using DataAccess.DTOs.Devices;

namespace API.Interfaces
{
    public interface IDeviceService
    {
        Task AddDeviceType(DeviceType deviceType);
        Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices);
    }
}
