using DataAccess.Models;

namespace API.Interfaces
{
    public interface ITemperatureSensorService
    {
        Task AddSensorDataAsync(TemperatureData sensorData);
        Task<TemperatureData> GetSensorDataByIdAsync(int id);
    }
}
