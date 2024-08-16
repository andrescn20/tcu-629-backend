using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class SensorsController : ControllerBase
    {
        private readonly ITemperatureSensorService _sensorService;
        public SensorsController(ITemperatureSensorService sensorDataService)
        {
            _sensorService = sensorDataService;
        }

        [HttpPost]
        public IActionResult Temperature(TemperatureData data)
        {
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<TemperatureData>> PostSensorData(TemperatureData sensorData)
        {
            if (sensorData == null)
            {
                return BadRequest("SensorData is null.");
            }

            await _sensorService.AddSensorDataAsync(sensorData);
            return sensorData;
            //return CreatedAtAction(nameof(GetSensorData), new { id = sensorData.Id }, sensorData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorData(int id)
        {
            var sensorData = await _sensorService.GetSensorDataByIdAsync(id);
            if (sensorData == null)
            {
                return NotFound();
            }
            return Ok(sensorData);
        }

        [HttpGet]
        public string TestSensorsController()
        {
            return "Sensors Controller is Working";
        }
    }
}
