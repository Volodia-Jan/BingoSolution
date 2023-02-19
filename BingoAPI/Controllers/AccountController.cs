using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Filters.TypeFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BingoAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [UserNotAuthorize]
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Returns logged in user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    public async Task<ActionResult<UserResponse>> LoginUser(LoginDto loginDto)
        => await _accountService.Login(loginDto);

    [UserNotAuthorize]
    [HttpPost("register")]
    [SwaggerOperation(Summary = "Returns registered user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    public async Task<ActionResult<UserResponse>> RegisterUser(RegisterDto registerDto)
        => await _accountService.Register(registerDto);

    [AuthorizeUser]
    [HttpGet("sign-out")]
    [SwaggerOperation(Summary = "Performs a user exit")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    public async Task<ActionResult> SignUserOut()
    {
        await _accountService.SignOut();

        return Ok();
    }
}
