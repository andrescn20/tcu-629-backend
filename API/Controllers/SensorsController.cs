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
        public IActionResult Temperature(SensorData data)
        {
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostSensorData(SensorData sensorData)
        {
            if (sensorData == null)
            {
                return BadRequest("SensorData is null.");
            }

            await _sensorService.AddSensorDataAsync(sensorData);
            return CreatedAtAction(nameof(GetSensorData), new { id = sensorData.SensorId }, sensorData);
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

    }
}
