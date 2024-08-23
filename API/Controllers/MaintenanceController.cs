using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [EnableCors("TCU_Cors")]
    [ApiController]
    [Route("/")]
    public class MaintenanceController : ControllerBase
    {
        [HttpGet]
        public string TestApi()
        {
            DateTime currentDateTime = DateTime.Now;
            string stringDate = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return $"API online. Currently: {stringDate}";
        }
    }
}
