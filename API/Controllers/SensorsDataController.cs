using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using DTO;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]/[action]")]
    public class SensorsDataController : ControllerBase
    {
        private readonly ICollectedDataService _collectedDataService;
        public SensorsDataController(ICollectedDataService collectedDataService)
        {
            _collectedDataService = collectedDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Temperature(TemperatureDataDto sensorData)
        {
            if (sensorData == null)
            {
                return BadRequest("Sensor Data is null.");
            }

            try
            {
                var response = await _collectedDataService.AddTemperatureDataAsync(sensorData);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TemperatureDataDto>>> GetTemperatureDataByDeviceId(int deviceId = -1)
        {
            if(deviceId == -1 )
            {
                return BadRequest("Device Id is necessary");
            }

            var response = await _collectedDataService.GetTemperatureDataByDeviceId(deviceId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<TemperatureDataDto>>> GetTemperatureDataBySensorId(int sensorId = -1)
        {
            if (sensorId == -1)
            {
                return BadRequest("Device Id is necessary");
            }

            var response = await _collectedDataService.GetTemperatureDataBySensorId(sensorId);
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult<List<TemperatureDataDto>>> GetAllTemperatureMeasurements()
        {
            return await _collectedDataService.GetAllTemperatureMeasurements();
        }

        [HttpGet]
        public string TestSensorsDataController()
        {
            return "Sensors Data Controller is Working";
        }
    }
}