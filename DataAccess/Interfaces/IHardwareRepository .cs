using DTO;
using DataAccess.Models;
namespace DataAccess.Interfaces
{
    public interface IHardwareRepository
    {
        Task<int> AddDeviceTypeAsync(DeviceType deviceType);
        Task<int> AddSensorTypeAsync(SensorType sensorType);
        Task<List<DeviceTypeDto>> GetDeviceTypesAsync(bool withDevices);
        Task<List<SensorTypeDto>> GetSensorTypesAsync();
        Task<int> AddDeviceAsync(Device device);
        Task<int> AddBoardAsync(Board board);
        Task AddSensorsAsync(List<Sensor> sensors);
        Task DeleteSensorTypeById(int sensorTypeId);
        Task DeleteDeviceTypeById(int deviceTypeId);
        Task<int> GetSensorIdByAddressAsync(string sensorAddress);
        Task<List<SensorDto>> GetAllSensors();
        Task<List<BoardDto>> GetAllBoards();

    }
}
