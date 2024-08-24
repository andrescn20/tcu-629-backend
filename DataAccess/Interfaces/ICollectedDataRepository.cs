using DTO;
using DataAccess.Models;
namespace DataAccess.Interfaces
{
    public interface ICollectedDataRepository
    {
        Task<TemperatureData> SaveTemperatureSensorDataAsync(TemperatureData temperatureData);

    }
}
