using BingoAPI.Core.Dtos;

namespace BingoAPI.Core.ServiceContracts;
public interface IUsersService
{
    Task<List<UserResponse>> GetAllUsers();

    Task<List<UserResponse>> GetUsersOrderedByGameSchedule(string userName);

    Task<List<UserResponse>> GetMatchesGameSchedule(string userName, string? gameTime = null);

    Task<UserResponse> SetGameSchedule(string username, string gameTime);

    Task<UserResponse> SetGamePrivacy(string username, bool privacy);
}
