using System.Security.Claims;
using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SecuritiesController: ControllerBase
{
    private const string GetSecurityByIdName = "GetSecurityById";
    
    private readonly SecurityService _securityService;

    public SecuritiesController(SecurityService securityService) {
        _securityService = securityService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSecurity([FromBody] CreateSecurityRequest security)
    {
        var createdSecurity = await _securityService.AddSecurity(security);
        return CreatedAtRoute(
            GetSecurityByIdName,
            new { id = createdSecurity.Id },
            createdSecurity
        );
    }

    [HttpGet("{id}", Name = GetSecurityByIdName)]
    public async Task<IActionResult> GetSecurityById([FromRoute] uint id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var fetchedSecurity = await _securityService.GetSecurityById(id, userId);
        return fetchedSecurity == null ? 
            NotFound() :
            Ok(fetchedSecurity);
    }
    
    //public async Task<IActionResult> GetOperations
}
