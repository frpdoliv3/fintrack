using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Operation.Authorization;

public static class OperationAuthorization
{
    public const string ChangeOperationPolicy = "ChangeOperationPolicy";
    
    public class ChangeOperationRequirement : IAuthorizationRequirement { }
}