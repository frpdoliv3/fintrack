using FinTrack.Application.Security.CreateSecurity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurityController
{
    private readonly CreateSecurityValidator _validator;

    public SecurityController(CreateSecurityValidator validator)
    {
        _validator = validator;
    }

    [HttpPost]
    public async Task<ValidationResult> CreateSecurity([FromBody] CreateSecurityRequest security)
    {
        return await _validator.ValidateAsync(security);
    }
}
