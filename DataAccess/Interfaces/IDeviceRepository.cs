using DataAccess.Models;
using DTO;
namespace DataAccess.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<DeviceDto>> GetDevicesAsync();
        Task<int> DeleteDeviceByIdAsync(int deviceId);
        Task<int> AddDeviceTypeAsync(DeviceType deviceType);
        Task<List<DeviceTypeDto>> GetDeviceTypesAsync(bool withDevices);
        Task DeleteDeviceTypeById(int deviceTypeId);
        Task<int> AddDeviceAsync(Device device);

    }
}
