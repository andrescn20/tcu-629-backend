using DataAccess.Models;
using DTO;

namespace DataAccess.Repositories
{
    public interface ISensorRepository 
    {
        Task DeleteSensorByIdAsync(int sensorId);
        Task AddSensorAsync(Sensor sensor);
        Task<int> AddSensorTypeAsync(SensorType sensorType);
        Task DeleteSensorTypeById(int sensorTypeId);
        Task<List<SensorTypeDto>> GetSensorTypesAsync();
        Task<int> GetSensorIdByAddressAsync(string sensorAddress);
        Task<Sensor> GetSensorByAddressAsync(string sensorAddress);
        Task<List<SensorDto>> GetAllSensors();
        Task<Sensor> GetSensorByIdAsync(int sensorId);
        Task<List<Sensor>> GetSensorsByIdAsync(List<int> sensorIds);
        Task UpdateSensorsAsync(List<Sensor> sensors);
   }
}
