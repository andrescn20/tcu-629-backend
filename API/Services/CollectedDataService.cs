using API.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DTO;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

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
                    throw new Exception("No se recibió información");
                }

                {
                    var sensor = await _sensorRepository.GetSensorByAddressAsync(sensorData.SensorAddress);
                    if (sensor == null)
                    {
                        throw new Exception("Sensor no registrado");
                    }

                    if (sensorData.Temperature < -20)
                    {
                        throw new Exception("Lectura de Temperatura incorrecta");
                    }
                    var temperatureSensorDto = new TemperatureData
                    {
                        SensorId = sensor.SensorId,
                        Temperature = sensorData.Temperature,
                        Timestamp = DateTime.SpecifyKind(sensorData.Timestamp, DateTimeKind.Utc),
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TemperatureDataDto>> GetTemperatureDataByDeviceId(int deviceId)
        {
            var device = await _collectedDataRepository.GetTemperatureDataByDeviceIdAsync(deviceId);

            List<TemperatureDataDto> temperatures = new List<TemperatureDataDto>();

            if (device != null)
            {
                foreach (var board in device.Boards)
                {
                    foreach (var sensor in board.Sensors)
                    {
                        foreach (var measure in sensor.TemperatureDataList)
                        {
                            var data = new TemperatureDataDto
                            {
                                Id = measure.Id,
                                SensorId = measure.SensorId,
                                Temperature = measure.Temperature,
                                Timestamp = measure.Timestamp,
                                BoardId = board.BoardId,
                                DeviceId = device.DeviceId,
                                SensorAddress = sensor.SensorAddress,
                            };

                            temperatures.Add(data);

                        }
                    }
                }
            }
            var sortedTemperatures = temperatures.OrderBy(t => t.Timestamp).ToList();
            return sortedTemperatures;

        }

        public async Task<List<TemperatureDataDto>> GetTemperatureDataBySensorId(int sensorId)
        {
            var sensor = await _collectedDataRepository.GetTemperatureDataBySensorIdAsync(sensorId);

            List<TemperatureDataDto> temperatures = new List<TemperatureDataDto>();

            if (sensor != null)
            {

                foreach (var measure in sensor.TemperatureDataList)
                {
                    var data = new TemperatureDataDto
                    {
                        Id = measure.Id,
                        SensorId = measure.SensorId,
                        Temperature = measure.Temperature,
                        Timestamp = measure.Timestamp,
                        BoardId = sensor.BoardId,
                        SensorAddress = sensor.SensorAddress,
                    };

                    temperatures.Add(data);


                }
            }

            return temperatures;

        }
        public async Task<List<TemperatureDataDto>> GetAllTemperatureMeasurements()
        {
            var measurements = await _collectedDataRepository.GetAllTemperatureMeasurementsAsync();
            List<TemperatureDataDto> temperatures = new List<TemperatureDataDto>();
            foreach (var measure in measurements)
            {
                var tempDto = new TemperatureDataDto
                {
                    Id = measure.Id,
                    SensorId = measure.SensorId,
                    Temperature = measure.Temperature,
                    SensorAddress = measure.Sensor.SensorAddress,
                    Timestamp = measure.Timestamp
                };

                temperatures.Add(tempDto);
            }
            return temperatures;
        }

        public async Task<DeviceStatsDto> GetTemperatureStatsByDevice(int deviceId)
        {
            var allTemperatures = await GetTemperatureDataByDeviceId(deviceId);

            List<TemperatureDataDto>? temperatures = null;

            var currentDate = DateTime.UtcNow;
            var counter = 1;

            while (temperatures == null || temperatures.Count == 0)
            {
                temperatures = allTemperatures
                    .Where(t => t.Timestamp.AddDays(30 * counter) >= currentDate)
                    .ToList();

                counter++;  
            }

            var sortedTemperatures = temperatures.OrderBy(t => t.Temperature).ToList();

            var MinTemperatureData = sortedTemperatures.FirstOrDefault();

            var MaxTemperatureData = temperatures.OrderByDescending(t => t.Temperature).FirstOrDefault();

            var LatestTemperatureData = temperatures.OrderByDescending(t => t.Timestamp).FirstOrDefault();

            int count = sortedTemperatures.Count;

            double medianTemperature;

            if (count % 2 == 1)
            {
                medianTemperature = (double)sortedTemperatures[count / 2].Temperature;
            }
            else
            {
                medianTemperature = (double)(sortedTemperatures[(count / 2) - 1].Temperature + sortedTemperatures[count / 2].Temperature) / 2.0;
            }


            var stats = new DeviceStatsDto
            {
                DeviceId = deviceId,
                MaxTemperature = (double)MaxTemperatureData.Temperature,
                MaxTemperatureTime = MaxTemperatureData.Timestamp,
                MedianTemperature = medianTemperature,
                MinTemperature = (double)MinTemperatureData.Temperature,
                MinTemperatureTime = MinTemperatureData.Timestamp,
                LatestTemperature = (double)LatestTemperatureData.Temperature,
                LatestTemperatureTime = LatestTemperatureData.Timestamp,

            };

            return stats;
        }

    }

}
