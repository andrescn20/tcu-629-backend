using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface ISensorService
    {
        Task DeleteSensorById(int sensorId);
    }
}
