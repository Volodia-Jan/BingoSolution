using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Filters.TypeFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers() 
        => await _usersService.GetAllUsers();

    [AuthorizeUser]
    [HttpGet("game-schedule")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetGameSchedule()
    {
        var userName = User.Identity?.Name;

        return await _usersService.GetUsersOrderedByGameSchedule(userName);
    }

    [AuthorizeUser]
    [HttpPatch("game-schedule/update")]
    public async Task<ActionResult<UserResponse>> UpdateUserGameSchedule([FromBody]string gameTime)
    {
        var userName = User.Identity?.Name;

        return await _usersService.SetGameSchedule(userName, gameTime);
    }
}
