using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using API.Services;

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
    }
}
