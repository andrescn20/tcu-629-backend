using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using API.Interfaces;
using DTO;
using API.Services;

namespace API.Controllers
{
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
            if (devices == null)
            {
                return NotFound();
            }
            return devices;
        }

        [HttpGet]
        public Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices = false)
        {
            return _deviceService.GetDeviceTypes(withDevices);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewDevice(NewDeviceDto device)
        {
            try
            {
                var newDevice = await _deviceService.CreateNewDevice(device);
                return Ok(newDevice);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddDeviceType(DeviceTypeDto deviceType)
        {
            try
            {

                var newType = await _deviceService.AddDeviceType(deviceType);
                return Ok(newType);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDevice(int deviceId)
        {
            await _deviceService.DeleteDeviceById(deviceId);
            return Ok($"Sensor type with ID {deviceId} deleted successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeviceType(int typeId)
        {
            await _deviceService.DeleteDeviceType(typeId);
            return Ok($"Device type with ID {typeId} deleted successfully.");
        }
    }
}
