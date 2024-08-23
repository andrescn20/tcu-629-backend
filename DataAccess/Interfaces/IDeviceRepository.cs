using DataAccess.Models;
using DTO;
namespace DataAccess.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<DeviceDto>> GetDevicesAsync();
        Task<int> DeleteDeviceByIdAsync(int deviceId);

    }
}
