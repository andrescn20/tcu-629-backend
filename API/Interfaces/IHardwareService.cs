using DataAccess.Models;
using DTO;
namespace API.Interfaces
{
    public interface IHardwareService
    {
        Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices);
        Task<List<SensorTypeDto>> GetSensorTypes();
        Task<DeviceDto> CreateNewDevice(HardwareDto device);
        Task<int> AddDeviceType(DeviceTypeDto deviceType);
        Task<int> AddSensorType(SensorTypeDto deviceType);
        Task DeleteSensorType(int typeId);
        Task DeleteDeviceType(int typeId);
        Task<List<SensorDto>> GetAllSensors();
        Task<List<BoardDto>> GetAllBoards();


    }
}
