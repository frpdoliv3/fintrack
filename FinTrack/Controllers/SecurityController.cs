using FinTrack.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurityController: ControllerBase
{
    private ILogger<SecurityController> _logger;

    public SecurityController(ILogger<SecurityController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    public void CreateSecurity([FromBody] Security security)
    {
        _logger.LogInformation(security.Isin);
    }
}