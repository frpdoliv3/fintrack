using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Operation.Authorization;

public class OperationAuthorization
{
    public const string ChangeOperationPolicy = "ChangeOperationPolicy";
    
    public class AuthorRequirement : IAuthorizationRequirement { }
}