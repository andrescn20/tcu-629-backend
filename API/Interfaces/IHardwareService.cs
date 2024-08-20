using DataAccess.Models;
using DTO;
using System;
namespace API.Interfaces
{
    public interface IHardwareService
    {
        Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices);

        Task<List<SensorTypeDto>> GetSensorTypes();
        Task<DeviceDto> CreateNewDevice(HardwareDto device);
        Task<int> AddDeviceType(DeviceTypeDto deviceType);
        Task<int> AddSensorType(SensorTypeDto deviceType);
        Task DeleteSensorType(int typeId);
        Task DeleteDeviceType(int typeId);

    }
}
