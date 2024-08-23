using DTO;
namespace API.Interfaces
{
    public interface IDeviceService
    {
        Task<List<DeviceDto>> GetAllDevices();
        Task<int> DeleteDeviceById(int deviceId);
    }
}
