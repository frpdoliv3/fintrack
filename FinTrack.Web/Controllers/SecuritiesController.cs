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
    public async Task<IResult> CreateSecurity([FromBody] CreateSecurityRequest security)
    {
        var createdSecurity = await _securityService.AddSecurity(security);
        return Results.CreatedAtRoute(
            GetSecurityByIdName,
            new { id = createdSecurity.Id },
            createdSecurity
        );
    }

    [HttpGet("{id}", Name = GetSecurityByIdName)]
    public async Task<IResult> GetSecurityById([FromRoute] uint id)
    {
        var fetchedSecurity = await _securityService.GetSecurityById(id);
        return fetchedSecurity == null ? 
            Results.NotFound() :
            Results.Ok(fetchedSecurity);
    }
}
