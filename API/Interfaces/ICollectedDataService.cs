using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface ICollectedDataService
    {
        Task<TemperatureDataDto> AddTemperatureDataAsync(TemperatureDataDto sensorData);
    }
}

