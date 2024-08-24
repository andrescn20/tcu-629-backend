using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface ISensorService
    {
        Task DeleteSensorById(int sensorId);
        Task<List<SensorTypeDto>> GetSensorTypes();
        Task<int> AddSensorType(SensorTypeDto deviceType);
        Task<List<SensorDto>> GetAllSensors();
        Task DeleteSensorType(int typeId);
        Task AddSensor(SensorDto sensor);
    }
}
