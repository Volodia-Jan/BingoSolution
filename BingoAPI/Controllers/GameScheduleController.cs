using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Filters.TypeFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BingoAPI.Controllers;

[AuthorizeUser]
[ApiController]
[Route("game-schedule")]
public class GameScheduleController : ControllerBase
{
    private readonly IUsersService _usersService;

    public GameScheduleController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Returns all users sorted by game schedule")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetGameSchedule()
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetUsersOrderedByGameSchedule(userName);
    }

    [HttpGet("matches")]
    [SwaggerOperation(Summary = "Returns all users that are matches with authorized user game schedule if it's null it uses current date")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetMatchesGameSchedule()
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetMatchesGameSchedule(userName);
    }

    [HttpPost("matches")]
    [SwaggerOperation(Summary = "Returns all users that are matched with given date")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetMatchesGameSchedule(GameScheduleDto gameSchedule)
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetMatchesGameSchedule(userName, gameSchedule.GameTime);
    }

    [HttpPatch("update")]
    [SwaggerOperation(Summary = "Sets a game schedule of authorized user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<UserResponse>> UpdateUserGameSchedule([FromBody]GameScheduleDto gameSchedule)
    {
        var userName = User.Identity?.Name;

        return await _usersService.SetGameSchedule(userName, gameSchedule.GameTime);
    }

    [HttpPatch("update/{isPrivate}")]
    [SwaggerOperation(Summary = "Sets a game privace of authorized user")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(UserResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<UserResponse>> ChangeGameSchedulePrivacy(bool isPrivate)
    {
        var userName = User.Identity?.Name;

        return await _usersService.SetGamePrivacy(userName, isPrivate);
    }
}
