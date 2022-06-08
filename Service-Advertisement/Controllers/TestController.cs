using Microsoft.AspNetCore.Mvc;

namespace Service_Advertisement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            return "test completed";
        }
    }
}
