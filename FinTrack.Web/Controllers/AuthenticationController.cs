using FinTrack.Application.Authentication.LoginUser;
using FinTrack.Application.Authentication.RegisterUser;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.Web.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthRepository _authRepo;
    private readonly SignInManager<EFUser> _signInManager;

    public AuthenticationController(IAuthRepository authRepo, SignInManager<EFUser> signInManager)
    {
        _authRepo = authRepo;
        _signInManager = signInManager;
        _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
    }

    [HttpPost("[action]")]
    public async Task Register([FromBody] CreateUser registerRequest)
    {
        await _authRepo.RegisterUser(registerRequest);
    }

    [HttpPost("[action]")]
    public async Task Login([FromBody] LoginUserRequest userIdentity)
    {
        var user = await _authRepo.FindUserByEmail(userIdentity.Identity);
        string userName;
        if (user == null)
        {
            userName = userIdentity.Identity;
        } else {
            userName = user.UserName;
        }
        await _signInManager.PasswordSignInAsync(userName, userIdentity.Password, userIdentity.RememberMe, lockoutOnFailure: true);
    }
}
