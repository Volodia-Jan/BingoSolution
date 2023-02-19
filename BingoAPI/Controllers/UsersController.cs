using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Filters.TypeFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BingoAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Returns all users from Db")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers() 
        => await _usersService.GetAllUsers();

    [AuthorizeUser]
    [HttpGet("game-schedule")]
    [SwaggerOperation(Summary = "Returns all users sorted by game schedule")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetGameSchedule()
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetUsersOrderedByGameSchedule(userName);
    }

    [AuthorizeUser]
    [HttpGet("game-schedule/matches")]
    [SwaggerOperation(Summary = "Returns all users that are matches with authorized user game schedule if it's null it uses current date")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetMatchesGameSchedule()
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetMatchesGameSchedule(userName);
    }

    [AuthorizeUser]
    [HttpPost("game-schedule/matches")]
    [SwaggerOperation(Summary = "Returns all users that are matched with given date")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetMatchesGameSchedule([FromQuery]string gameTime)
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetMatchesGameSchedule(userName, gameTime);
    }

    [AuthorizeUser]
    [HttpPatch("game-schedule/update")]
    [SwaggerOperation(Summary = "Sets a game schedule of authorized user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<ActionResult<UserResponse>> UpdateUserGameSchedule([FromForm]string gameTime)
    {
        var userName = User.Identity?.Name;

        return await _usersService.SetGameSchedule(userName, gameTime);
    }
}
