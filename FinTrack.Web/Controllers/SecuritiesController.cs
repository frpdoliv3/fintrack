using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Security.EditSecurity;
using Microsoft.AspNetCore.JsonPatch;
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
            new { securityId = createdSecurity.Id },
            createdSecurity
        );
    }

    [HttpGet("{securityId}", Name = GetSecurityByIdName)]
    public async Task<IActionResult> GetSecurityById([FromRoute] uint securityId)
    {
        var fetchedSecurity = await _securityService.GetSecurityById(User, securityId);
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
        try
        {
            var operations = await _securityService
                .GetOperationsForId(User, securityId, pageNumber, pageSize);
            return Ok(operations);
        }
        catch (UnauthorizedAccessException)
        {
            return NotFound();
        }
    }

    [HttpGet("{securityId}/status")]
    public async Task<IActionResult> GetStatus([FromRoute] uint securityId)
    {
        try
        {
            return Ok(await _securityService.GetOperationStatus(User, securityId));
        }
        catch (UnauthorizedAccessException)
        {
            return NotFound();
        }
    }

    [HttpPut("{securityId}")]
    public async Task<IActionResult> UpdateSecurity(
        ulong securityId,
        [FromBody] EditSecurityRequest security
    )
    {
        security.Id = securityId;
        var updatedSecurity = await _securityService.UpdateSecurity(User, security);
        if (updatedSecurity == null)
        {
            return NotFound();
        }

        return Ok(updatedSecurity);
    }

    [HttpDelete("{securityId}")]
    public async Task<IActionResult> DeleteSecurity([FromRoute] ulong securityId)
    {
        var deleteResult = await _securityService.DeleteSecurity(User, securityId);
        if (!deleteResult)
        {
            return NotFound();
        }
        return NoContent();
    }
}
