using API.Interfaces;
using DataAccess.Models;
using DataAccess.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class HardwareService : IHardwareService
    {

        private readonly IHardwareRepository _hardwareRepository;

        public HardwareService(IHardwareRepository hardwareRepository)
        {
            _hardwareRepository = hardwareRepository;
        }

        public async Task<List<SensorDto>> GetAllSensors()
        {
            return await _hardwareRepository.GetAllSensors();
        }
        public async Task<List<BoardDto>> GetAllBoards()
        {
            return await _hardwareRepository.GetAllBoards();

        }
        public async Task<int> AddDeviceType(DeviceTypeDto deviceType)
        {
            var newDeviceType = new DeviceType
            {
                Type = deviceType.Type
            };
            return await _hardwareRepository.AddDeviceTypeAsync(newDeviceType);
        }
        public async Task DeleteDeviceType(int deviceTypeId)
        {
            await _hardwareRepository.DeleteDeviceTypeById(deviceTypeId);
        }

        public async Task<int> AddSensorType(SensorTypeDto sensorType)
        {
            var newSensorType = new SensorType
            {
                Type = sensorType.Type
            };
           return await _hardwareRepository.AddSensorTypeAsync(newSensorType);
        }
        public async Task DeleteSensorType(int sensorTypeId)
        {
            await _hardwareRepository.DeleteSensorTypeById(sensorTypeId);
        }

        public async Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices)
        {
            return await _hardwareRepository.GetDeviceTypesAsync(withDevices);
        }

        public async Task<List<SensorTypeDto>> GetSensorTypes()
        {
            return await _hardwareRepository.GetSensorTypesAsync();
        }

        public async Task<DeviceDto> CreateNewDevice(HardwareDto device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            try
            {
                var newDevice = new Device
                {
                    DeviceTypeId = device.DeviceTypeId,
                    Description = device.Description,
                    Location = device.Location,
                    Added_at = DateTime.UtcNow,
                };

                int deviceId = await _hardwareRepository.AddDeviceAsync(newDevice);

                var newBoard = new Board
                {
                    DeviceId = deviceId,
                    Microcontroller = device.Board.Microcontroller,
                    Description = device.Board.Description,
                };

                int boardId = await _hardwareRepository.AddBoardAsync(newBoard);

                var newSensors = device.Sensors.Select(s => new Sensor
                {
                    SensorName = s.SensorName,
                    Description = s.Description,
                    BoardId = boardId,
                    SensorTypeId = s.SensorTypeId,
                }).ToList();

                await _hardwareRepository.AddSensorsAsync(newSensors);

                return new DeviceDto
                {
                    DeviceId = newDevice.DeviceId,
                    DeviceTypeId = newDevice.DeviceTypeId,
                    Description = newDevice.Description,
                    Location = newDevice.Location,
                    Added_at = newDevice.Added_at,
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the device.", ex);
            }
        }
    }
}
