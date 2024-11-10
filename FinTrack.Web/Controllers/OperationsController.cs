using System.Security.Claims;
using FinTrack.Application.Operation.Authorization;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController: ControllerBase
{
    private readonly IAuthorizationService _authService;
    private readonly ISecurityRepository _securityRepo;
    
    public OperationsController(IAuthorizationService authService, ISecurityRepository securityRepo)
    {
        _authService = authService;
        _securityRepo = securityRepo;
    }
    
    [HttpDelete("{operationId}")]
    public async Task<IActionResult> DeleteOperation(ulong operationId)
    {
        var operation = await _securityRepo
            .GetOperationById(operationId);
        if (operation == null)
        {
            return NotFound();
        }

        var authResult = await _authService
            .AuthorizeAsync(User, operation, OperationAuthorization.ChangeOperationPolicy);
        if (!authResult.Succeeded)
        {
            return NotFound();
        }
        await _securityRepo.DeleteOperation(operation);
        return Ok();
    }
}