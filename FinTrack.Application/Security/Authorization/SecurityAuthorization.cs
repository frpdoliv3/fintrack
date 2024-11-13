using Microsoft.AspNetCore.Authorization;

namespace FinTrack.Application.Security.Authorization;

public static class SecurityAuthorization
{
    public const string ViewSecurityPolicy = "ViewSecurityDetailsPolicy";
    public const string ChangeSecurityPolicy = "ChangeSecurityDetailsPolicy";

    public class ViewSecurityRequirement : IAuthorizationRequirement;
    public class ChangeSecurityRequirement : IAuthorizationRequirement;
}