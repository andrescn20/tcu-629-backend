using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface ICollectedDataService
    {
        Task<TemperatureDataDto> AddTemperatureDataAsync(TemperatureDataDto sensorData);
        Task<List<TemperatureDataDto>> GetTemperatureDataByDeviceId(int deviceId);
        Task<List<TemperatureDataDto>> GetTemperatureDataBySensorId(int sensorId);
        Task<List<TemperatureDataDto>> GetAllTemperatureMeasurements();
        Task<DeviceStatsDto> GetTemperatureStatsByDevice(int deviceId);
    }
}

