using DTO;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interfaces
{
    public interface IHardwareRepository
    {
        Task<int> AddDeviceTypeAsync(DeviceType deviceType);
        Task<int> AddSensorTypeAsync(SensorType sensorType);
        Task<List<DeviceTypeDto>> GetDeviceTypesAsync(bool withDevices);
        Task<List<SensorTypeDto>> GetSensorTypesAsync();
        Task<int> AddDeviceAsync(Device device);
        Task<int> AddBoardAsync(Board board);
        Task AddSensorsAsync(List<Sensor> sensors);
        Task DeleteSensorTypeById(int sensorTypeId);
        Task DeleteDeviceTypeById(int deviceTypeId);
    }
}
