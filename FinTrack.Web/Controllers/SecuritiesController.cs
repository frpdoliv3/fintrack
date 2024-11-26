using FinTrack.Application.Security;
using FinTrack.Application.Security.CreateSecurity;
using FinTrack.Application.Security.EditSecurity;
using FinTrack.Application.Security.GetOperations;
using FinTrack.Application.Security.GetSecurity;
using FinTrack.Application.Security.GetSecurityStatus;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UnauthorizedAccessException = FinTrack.Domain.Exceptions.UnauthorizedAccessException;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SecuritiesController: ControllerBase
{
    private const string GetSecurityByIdName = "GetSecurityById";
    
    private readonly CreateSecurityUseCase _createSecurityUseCase;
    private readonly GetSecurityByIdUseCase _getSecurityByIdUseCase;
    private readonly GetOperationsForIdUseCase _getOperationsForIdUseCase;
    private readonly GetSecurityStatusUseCase _getSecurityStatusUseCase;
    private readonly EditSecurityUseCase _editSecurityUseCase;
    private readonly DeleteSecurityUseCase _deleteSecurityUseCase;
    
    public SecuritiesController(
        CreateSecurityUseCase createSecurityUseCase,
        GetSecurityByIdUseCase getSecurityByIdUseCase,
        GetOperationsForIdUseCase getOperationsForIdUseCase,
        GetSecurityStatusUseCase getSecurityStatusUseCase,
        EditSecurityUseCase editSecurityUseCase,
        DeleteSecurityUseCase deleteSecurityUseCase
    )
    {
        _createSecurityUseCase = createSecurityUseCase;
        _getSecurityByIdUseCase = getSecurityByIdUseCase;
        _getOperationsForIdUseCase = getOperationsForIdUseCase;
        _getSecurityStatusUseCase = getSecurityStatusUseCase;
        _editSecurityUseCase = editSecurityUseCase;
        _deleteSecurityUseCase = deleteSecurityUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSecurity([FromBody] CreateSecurityRequest security)
    {
        var createdSecurity = await _createSecurityUseCase.Execute(security);
        return CreatedAtRoute(
            GetSecurityByIdName,
            new { securityId = createdSecurity.Id },
            createdSecurity
        );
    }

    [HttpGet("{securityId}", Name = GetSecurityByIdName)]
    public async Task<IActionResult> GetSecurityById([FromRoute] uint securityId)
    {
        var fetchedSecurity = await _getSecurityByIdUseCase.Execute(User, securityId);
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
            var operations = await _getOperationsForIdUseCase
                .Execute(User, securityId, pageNumber, pageSize);
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
            return Ok(await _getSecurityStatusUseCase.Execute(User, securityId));
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
        var updatedSecurity = await _editSecurityUseCase.Execute(User, security);
        if (updatedSecurity == null)
        {
            return NotFound();
        }

        return Ok(updatedSecurity);
    }

    [HttpDelete("{securityId}")]
    public async Task<IActionResult> DeleteSecurity([FromRoute] ulong securityId)
    {
        var deleteResult = await _deleteSecurityUseCase.Execute(User, securityId);
        if (!deleteResult)
        {
            return NotFound();
        }
        return NoContent();
    }
}
