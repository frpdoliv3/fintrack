using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Server.Controllers
{
    [ApiController]
    [Route("security")]
    public class SecurityController : ControllerBase
    {
        [HttpGet(Name = "Hello")]
        public String Hello()
        {
            return "Hello World";
        }
    }
}
