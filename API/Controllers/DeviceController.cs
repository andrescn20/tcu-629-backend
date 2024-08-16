using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.DTOs.Devices;
using API.Interfaces;

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
        public Task<List<DeviceTypeDto>> GetDeviceTypes(bool withDevices = false)
        {
            return _deviceService.GetDeviceTypes(withDevices);
        }
    }
}
