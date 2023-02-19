using BingoAPI.Core.Dtos;

namespace BingoAPI.Core.ServiceContracts;
public interface IFriendshipsService
{
    Task<List<UserResponse>> GetAllUserFriends(string username);

    Task<List<UserResponse>> GetAllFriendRequests(string username);

    Task<bool> AddFriend(string username, string friendUsername);

    Task<bool> AcceptFriend(string username, string friendUsername);
}
