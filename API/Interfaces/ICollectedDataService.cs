using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface ICollectedDataService
    {
        Task AddTemperatureDataAsync(TemperatureDataDto sensorData);
    }
}

