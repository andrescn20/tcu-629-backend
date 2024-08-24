using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using DTO;

namespace API.Controllers
{
    [EnableCors("TCU_Cors")]
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
        public string TestSensorsDataController()
        {
            return "Sensors Data Controller is Working";
        }
    }
}