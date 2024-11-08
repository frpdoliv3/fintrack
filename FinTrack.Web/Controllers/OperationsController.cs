using System.Security.Claims;
using FinTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController: ControllerBase
{
    private readonly ISecurityRepository _securityRepo;
    
    public OperationsController(ISecurityRepository securityRepo)
    {
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

        if (operation.Security.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return Forbid();
        }
        await _securityRepo.DeleteOperation(operation);
        return Ok();
    }
}