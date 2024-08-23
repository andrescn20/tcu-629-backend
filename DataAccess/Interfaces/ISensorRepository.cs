using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface ISensorRepository 
    {
        Task DeleteSensorByIdAsync(int sensorId);

    }
}
