using DTO;
namespace API.Interfaces
{
    public interface IDeviceService
    {
        Task<List<DeviceDto>> GetAllDevices();
        Task<int> DeleteDeviceById(int deviceId);
        Task<DeviceDto> CreateNewDevice(NewDeviceDto device);
        Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices);
        Task<int> AddDeviceType(DeviceTypeDto deviceType);
        Task DeleteDeviceType(int typeId);
    }
}
