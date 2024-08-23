using API.Interfaces;
using DataAccess.Interfaces;
using DTO;

namespace API.Services
{
    public class DeviceService : IDeviceService
    {

        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<List<DeviceDto>> GetAllDevices()
        {
            return await _deviceRepository.GetDevicesAsync();
        }
        public async Task<int> DeleteDeviceById(int deviceId)
        {
            return await _deviceRepository.DeleteDeviceByIdAsync(deviceId);
        }
    }
}
