using System.Security.Claims;
using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;
using Microsoft.AspNetCore.Mvc;
using UnauthorizedAccessException = FinTrack.Domain.Exceptions.UnauthorizedAccessException;

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

    [HttpGet("{securityId}", Name = GetSecurityByIdName)]
    public async Task<IActionResult> GetSecurityById([FromRoute] uint securityId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var fetchedSecurity = await _securityService.GetSecurityById(securityId, userId);
        return fetchedSecurity == null ? 
            NotFound() :
            Ok(fetchedSecurity);
    }

    [HttpGet("{securityId}/operations")]
    public async Task<IActionResult> GetOperationsForId(
        [FromRoute] uint securityId,
        [FromQuery(Name = "page")] int pageNumber = 1, 
        [FromQuery(Name = "page_size")] int pageSize = 10
    ) {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        try
        {
            var operations = await _securityService
                .GetOperationsForId(userId, securityId, pageNumber, pageSize);
            return Ok(operations);
        }
        catch (UnauthorizedAccessException)
        {
            return NotFound();
        }
    }
}
