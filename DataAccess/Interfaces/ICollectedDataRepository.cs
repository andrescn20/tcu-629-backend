using DTO;
using DataAccess.Models;
namespace DataAccess.Interfaces
{
    public interface ICollectedDataRepository
    {
        Task<TemperatureData> SaveTemperatureSensorDataAsync(TemperatureData temperatureData);
        Task<Device> GetTemperatureDataByDeviceIdAsync(int deviceId);
        Task<Sensor> GetTemperatureDataBySensorIdAsync(int sensorId);
        Task<List<TemperatureData>> GetAllTemperatureMeasurementsAsync();

    }
}
