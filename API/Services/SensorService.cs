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
        public async Task<TemperatureData> GetSensorDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
