using API.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DTO;

namespace API.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public async Task DeleteSensorById(int sensorId)
        {
            await _sensorRepository.DeleteSensorByIdAsync(sensorId);
        }

        public async Task<List<SensorDto>> GetAllSensors()
        {
            return await _sensorRepository.GetAllSensors();
        }


        public async Task<int> AddSensorType(SensorTypeDto sensorType)
        {
            var newSensorType = new SensorType
            {
                Type = sensorType.Type
            };
            return await _sensorRepository.AddSensorTypeAsync(newSensorType);
        }
        public async Task DeleteSensorType(int sensorTypeId)
        {
            await _sensorRepository.DeleteSensorTypeById(sensorTypeId);
        }


        public async Task<List<SensorTypeDto>> GetSensorTypes()
        {
            return await _sensorRepository.GetSensorTypesAsync();
        }

        public async Task AddSensor(SensorDto sensor)
        {
            var sensorModel = new Sensor
            {
                Description = sensor.Description,
                IsAvailable = true,
                SensorName = sensor.SensorName,
                SensorTypeId = sensor.SensorTypeId,
                SensorAddress = sensor.SensorAddress,
            };
            await _sensorRepository.AddSensorAsync(sensorModel);
        }
    }

}
