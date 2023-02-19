using BingoAPI.Core.Dtos;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Filters.TypeFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BingoAPI.Controllers;

[Authorize]
[AuthorizeUser]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IFriendshipsService _friendshipsService;

    public UsersController(IUsersService usersService, IFriendshipsService friendshipsService)
    {
        _usersService = usersService;
        _friendshipsService = friendshipsService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Returns all users from Db")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers() 
        => await _usersService.GetAllUsers();

    [HttpGet("friends")]
    [SwaggerOperation(Summary = "Returns all authorized user friends")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUserFriends()
    {
        var username = User.Identity?.Name;

        return await _friendshipsService.GetAllUserFriends(username);
    }

    [HttpGet("friend-requests")]
    [SwaggerOperation(Summary = "Returns all authorized user friend-requests")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUserFriendRequests()
    {
        var username = User.Identity?.Name;

        return await _friendshipsService.GetAllFriendRequests(username);
    }

    [HttpPost("friends")]
    [SwaggerOperation(Summary = "Send friend request")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<ActionResult> AddFriend(FriendDto friendDto)
    {
        var username = User.Identity?.Name;

        bool isAdded = await _friendshipsService.AddFriend(username, friendDto.FriendUserName);

        return isAdded ? Ok("Successful sending friend request") : BadRequest("Something went wrong during sending friend request");
    }

    [HttpPatch("friends")]
    [SwaggerOperation(Summary = "Accept friend")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(IEnumerable<UserResponse>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request")]
    public async Task<ActionResult> AcceptFriend(FriendDto friendDto)
    {
        var username = User.Identity?.Name;

        bool isAccepted = await _friendshipsService.AcceptFriend(username, friendDto.FriendUserName);

        return isAccepted ? Ok("Successful operation") : BadRequest("Something went wrong during accepting friend request");
    }
}
