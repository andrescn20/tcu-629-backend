using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using API.Services;
using DTO;

namespace API.Controllers
{
    [EnableCors("TCU_Cors")]
    [ApiController]
    [Route("/[controller]/[action]")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        public SensorController(ISensorService sensorDataService)
        {
            _sensorService = sensorDataService;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSensorById(int sensorId)
        {

            await _sensorService.DeleteSensorById(sensorId);

            return Ok($"Sensor with id: {sensorId} deleted");
        }

        [HttpGet]
        public string TestSensorsController()
        {
            return "Sensors Controller is Working";
        }

        [HttpGet]
        public Task<List<SensorTypeDto>> GetSensorTypes()
        {
            return _sensorService.GetSensorTypes();
        }

        [HttpGet]
        public Task<List<SensorDto>> GetAllSensors()
        {
            return _sensorService.GetAllSensors();
        }
        [HttpPost]
        public async Task<IActionResult> AddSensorType(SensorTypeDto sensorType)
        {
            try
            {

                var newType = await _sensorService.AddSensorType(sensorType);
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
        public async Task<IActionResult> DeleteSensorType(int typeId)
        {
            await _sensorService.DeleteSensorType(typeId);
            return Ok($"Sensor type with ID {typeId} deleted successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSensor(SensorDto sensor)
        {
            await _sensorService.AddSensor(sensor);
            return Ok();
        }


    }
}
