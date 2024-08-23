using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using API.Interfaces;
using DTO;
using API.Services;

namespace API.Controllers
{
    [EnableCors("TCU_Cors")]
    [ApiController]
    [Route("/[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceDto>>> GetAllDevices()
        {
            var devices = await _deviceService.GetAllDevices();
            if(devices == null)
            {
                return NotFound();
            }
            return devices;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            await _deviceService.DeleteDeviceById(deviceId);
            return Ok($"Sensor type with ID {deviceId} deleted successfully.");
        }
    }
}
