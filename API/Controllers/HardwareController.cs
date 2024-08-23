using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using API.Services;

namespace API.Controllers
{
    [EnableCors("TCU_Cors")]
    [ApiController]
    [Route("/[controller]/[action]")]
    public class HardwareController : ControllerBase
    {
        private readonly IHardwareService _hardwareService;
        public HardwareController(IHardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }

        [HttpGet]
        public Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices = false)
        {
            return _hardwareService.GetDeviceTypes(withDevices);
        }

        [HttpGet]
        public Task<List<SensorTypeDto>> GetSensorTypes()
        {
            return _hardwareService.GetSensorTypes();
        }

        [HttpGet]
        public Task<List<SensorDto>> GetAllSensors()
        {
            return _hardwareService.GetAllSensors();
        }

        [HttpGet]
        public Task<List<BoardDto>> GetAllBoards()
        {
            return _hardwareService.GetAllBoards();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDevice(HardwareDto device) {
            try
            {

            var newDevice = await _hardwareService.CreateNewDevice(device);
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
        public async Task<IActionResult> AddSensorType(SensorTypeDto sensorType)
        {
            try
            {

                var newType = await _hardwareService.AddSensorType(sensorType);
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

        [HttpPost]
        public async Task<IActionResult> AddDeviceType(DeviceTypeDto deviceType)
        {
            try
            {

                var newType = await _hardwareService.AddDeviceType(deviceType);
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
        public async Task<IActionResult> DeleteTypes(string category, int typeId)
        {

            if (category.ToLower() == "sensor")
            {
                await _hardwareService.DeleteSensorType(typeId);
                return Ok($"Sensor type with ID {typeId} deleted successfully.");
            }

            if (category.ToLower() == "device")
            {
                await _hardwareService.DeleteDeviceType(typeId);
                return Ok($"Device type with ID {typeId} deleted successfully.");
            }

            return BadRequest("Invalid category. Only 'sensor' and 'device' are supported.");
        }
    }
}
