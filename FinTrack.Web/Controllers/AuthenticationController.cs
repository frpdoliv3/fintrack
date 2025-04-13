using FinTrack.Application.Authentication.LoginUser;
using FinTrack.Application.Authentication.SaveRefreshToken;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FinTrack.Web.Controllers;

// TODO: refactor this and move logic to another project
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthRepository _authRepo;
    private readonly SignInManager<EFUser> _signInManager;
    private readonly IOptionsMonitor<BearerTokenOptions> _bearerTokenOptions;
    private readonly TimeProvider _timeProvider;

    public AuthenticationController(
        IAuthRepository authRepo,
        SignInManager<EFUser> signInManager, 
        IOptionsMonitor<BearerTokenOptions> bearerTokenOptions,
        TimeProvider timeProvider
    ) {
        _authRepo = authRepo;
        _signInManager = signInManager;
        _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
        _bearerTokenOptions = bearerTokenOptions;
        _timeProvider = timeProvider;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task Register([FromBody] CreateUser registerRequest)
    {
        await _authRepo.Register(registerRequest);
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task Login([FromBody] LoginUserRequest userIdentity)
    {
        var user = await _authRepo.FindByEmail(userIdentity.Identity);
        string userName;
        if (user == null)
        {
            userName = userIdentity.Identity;
        } else {
            userName = user.UserName;
        }

        await _signInManager.PasswordSignInAsync(
            userName,
            userIdentity.Password, 
            userIdentity.RememberMe,
            lockoutOnFailure: true
        );
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshTokenCookie([FromBody] SaveRefreshTokenRequest refreshToken)
    {
        var refreshTicket = await GetRefreshToken(refreshToken.RefreshToken);
        if (refreshTicket == null)
        {
            return Unauthorized();
        }
        
        var refreshCookieOptions = new CookieOptions
        {
            // TODO: Make the token secure
            HttpOnly = true,
            Expires = refreshTicket.Properties.ExpiresUtc,
            Path = "/refresh"
        };
        Response.Cookies.Append("X-Refresh-Token", refreshToken.RefreshToken, refreshCookieOptions);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("[action]")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["X-Refresh-Token"];
        if (refreshToken == null)
        {
            return Unauthorized();
        }

        var refreshTicket = await GetRefreshToken(refreshToken);
        if (refreshTicket == null)
        {
            return Unauthorized();
        }
        
        return SignIn(refreshTicket.Principal, authenticationScheme: IdentityConstants.BearerScheme);
    }

    private async Task<AuthenticationTicket?> GetRefreshToken(string refreshToken)
    {
        var refreshTokenProtector = _bearerTokenOptions
            .Get(IdentityConstants.BearerScheme)
            .RefreshTokenProtector;
        
        var refreshTicket = refreshTokenProtector.Unprotect(refreshToken);
        // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
        if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
            _timeProvider.GetUtcNow() >= expiresUtc ||
            await _signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is null)

        {
            return null;
        }

        return refreshTicket;
    }
}
