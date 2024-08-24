using API.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace API.Services
{
    public class DeviceService : IDeviceService
    {

        private readonly IDeviceRepository _deviceRepository;
        private readonly ISensorRepository _sensorRepository;
        private readonly IBoardRepository _boardRepository;


        public DeviceService(
            IDeviceRepository deviceRepository,
            ISensorRepository sensorRepository,
            IBoardRepository boardRepository
            )
        {
            _deviceRepository = deviceRepository;
            _boardRepository = boardRepository;
            _sensorRepository = sensorRepository;

        }

        public async Task<List<DeviceDto>> GetAllDevices()
        {
            return await _deviceRepository.GetDevicesAsync();
        }
        public async Task<int> DeleteDeviceById(int deviceId)
        {
            return await _deviceRepository.DeleteDeviceByIdAsync(deviceId);
        }

        [HttpGet]
        public async Task<int> AddDeviceType(DeviceTypeDto deviceType)
        {
            var newDeviceType = new DeviceType
            {
                Type = deviceType.Type
            };
            return await _deviceRepository.AddDeviceTypeAsync(newDeviceType);
        }
        public async Task DeleteDeviceType(int deviceTypeId)
        {
            await _deviceRepository.DeleteDeviceTypeById(deviceTypeId);
        }
        public async Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices)
        {
            return await _deviceRepository.GetDeviceTypesAsync(withDevices);
        }

        public async Task<DeviceDto> CreateNewDevice(NewDeviceDto device)
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

                int deviceId = await _deviceRepository.AddDeviceAsync(newDevice);

                Board board = await _boardRepository.GetBoardByIdAsync(device.BoardId);

                if (board != null)
                {
                    board.DeviceId = deviceId;
                    board.IsInstalled = true;
                    await _boardRepository.UpdateBoardAsync(board);

                    List<Sensor> sensors = await _sensorRepository.GetSensorsByIdAsync(device.SensorIds);
                    if (sensors != null)
                    {
                        foreach (var sensor in sensors)
                        {
                            sensor.BoardId = board.BoardId;
                            sensor.IsAvailable = false;
                        }
                        await _sensorRepository.UpdateSensorsAsync(sensors);
                    }

                }

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
