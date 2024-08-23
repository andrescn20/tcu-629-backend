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
        private readonly IHardwareRepository _hardwareRepository;

        public CollectedDataService(ICollectedDataRepository collectedDataRepository,
            IHardwareRepository hardwareRepository)
        {
            _collectedDataRepository = collectedDataRepository;
            _hardwareRepository = hardwareRepository;
        }
        public async Task AddTemperatureDataAsync(TemperatureDataDto sensorData)
        {
            try
            {
                if (sensorData == null)
                {
                    throw new ArgumentNullException(nameof(sensorData));
                }

                {
                    var sensorId = await _hardwareRepository.GetSensorIdByAddressAsync(sensorData.SensorAddress);
                    if (sensorId == null)
                    {
                        throw new Exception("Sensor not found or is not ");
                    }

                    var temperatureSensorDto = new TemperatureData
                    {
                        SensorId = sensorId,
                        Temperature = sensorData.Temperature,
                        Timestamp = sensorData.Timestamp,
                    };

                    await _collectedDataRepository.SaveTemperatureSensorDataAsync(temperatureSensorDto);
                }
            }
            catch
            {
                throw new Exception("Error saving Data");
            }
        }
    }

}
