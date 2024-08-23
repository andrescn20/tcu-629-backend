using DTO;
using DataAccess.Models;
namespace DataAccess.Interfaces
{
    public interface ICollectedDataRepository
    {
        Task SaveTemperatureSensorDataAsync(TemperatureData temperatureData);

    }
}
