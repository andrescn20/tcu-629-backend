using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace API.Controllers
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public string Test()
        {
            return "Greetings from the tcu API";
        }
    }
}