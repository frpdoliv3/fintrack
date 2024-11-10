using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.Authorization;

public static class SecurityAuthorization
{
    public const string ViewSecurityPolicy = "ViewSecurityDetailsPolicy";
    
    public class SameAuthorRequirement: IAuthorizationRequirement;
}