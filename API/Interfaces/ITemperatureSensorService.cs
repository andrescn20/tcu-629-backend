using DataAccess.Models;

namespace API.Interfaces
{
    public interface ITemperatureSensorService
    {
        Task AddSensorDataAsync(SensorData sensorData);
        Task<SensorData> GetSensorDataByIdAsync(int id);
    }
}
