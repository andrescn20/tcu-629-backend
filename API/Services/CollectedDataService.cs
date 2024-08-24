using API.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DTO;

namespace API.Services
{
    public class CollectedDataService : ICollectedDataService
    {
        private readonly ICollectedDataRepository _collectedDataRepository;
        private readonly ISensorRepository _sensorRepository;

        public CollectedDataService(ICollectedDataRepository collectedDataRepository,
            ISensorRepository sensorRepository)
        {
            _collectedDataRepository = collectedDataRepository;
            _sensorRepository = sensorRepository;
        }
        public async Task<TemperatureDataDto> AddTemperatureDataAsync(TemperatureDataDto sensorData)
        {
            try
            {
                if (sensorData == null)
                {
                    throw new ArgumentNullException(nameof(sensorData));
                }

                {
                    var sensor = await _sensorRepository.GetSensorByAddressAsync(sensorData.SensorAddress);
                    if (sensor == null)
                    {
                        throw new Exception("Sensor not found");
                    }

                    var temperatureSensorDto = new TemperatureData
                    {
                        SensorId = sensor.SensorId,
                        Temperature = sensorData.Temperature,
                        Timestamp = sensorData.Timestamp,
                    };

                    var savedRecord = await _collectedDataRepository.SaveTemperatureSensorDataAsync(temperatureSensorDto);

                    return new TemperatureDataDto
                    {
                        Id = savedRecord.Id,
                        SensorId = savedRecord.SensorId,
                        Temperature = savedRecord.Temperature,
                        Timestamp = savedRecord.Timestamp,
                        SensorAddress = sensor.SensorAddress,
                        BoardId = sensor.BoardId,
                    };
                    
                }
            }
            catch
            {
                throw new Exception("Error saving Data");
            }
        }
    }

}
