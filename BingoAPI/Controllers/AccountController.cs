using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Returns logged in user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    public async Task<ActionResult<UserResponse>> LoginUser(LoginDto loginDto)
        => await _usersService.Login(loginDto);

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Returns registered user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    public async Task<ActionResult<UserResponse>> RegisterUser(RegisterDto registerDto)
        => await _usersService.Register(registerDto);

    [HttpGet("sign-out")]
    [SwaggerOperation(Summary = "Performs a user exit")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    public async Task<ActionResult> SignUserOut()
    {
        await _usersService.SignOut();

        return Ok();
    }
}
