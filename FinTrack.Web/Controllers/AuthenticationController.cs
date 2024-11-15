﻿using FinTrack.Application.Authentication.LoginUser;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
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
}
