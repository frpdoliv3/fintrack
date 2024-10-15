using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        

    }

    public record CreateCountryDto
    {
        public string Name
    }
}
