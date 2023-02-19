﻿using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BingoAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUsersService _usersService;

    public AccountController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> LoginUser(LoginDto loginDto)
        => await _usersService.Login(loginDto);

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> RegisterUser(RegisterDto registerDto)
        => await _usersService.Register(registerDto);

    [HttpGet("sign-out")]
    public async Task<ActionResult> SignUserOut()
    {
        await _usersService.SignOut();

        return Ok();
    }
}
