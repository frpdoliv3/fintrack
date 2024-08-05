using FinTrack.Server.Entity;
using FinTrack.Server.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace FinTrack.Server;
internal class CustomModelStateSerialization
{
    private APIErrorParserService _errorParserService;

    public CustomModelStateSerialization(APIErrorParserService errorParserService)
    {
        _errorParserService = errorParserService;
    }

    public IActionResult OnSerialize(ActionContext actionContext)
    {
        var errorRoot = _errorParserService.ParseErrors(actionContext.ModelState) as ObjectError;
        return new BadRequestObjectResult(JsonSerializer.Serialize(errorRoot));
    }
}
