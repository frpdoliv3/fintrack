using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurityController
{
    private readonly SecurityService _securityService;

    public SecurityController(SecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpPost]
    public async Task<IResult> CreateSecurity([FromBody] CreateSecurityRequest security)
    {
        await _securityService.AddSecurity(security);
        return Results.Created();
    }
}
