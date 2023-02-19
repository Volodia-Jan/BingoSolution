using BingoAPI.Core.Entities;

namespace BingoAPI.Core.RepositoryContracts;

public interface IFriendshipRepository
{
    Task<List<Friendship>> FindAllUserFriends(Guid userId);

    Task<List<Friendship>> FindAllFriendRequests(Guid userId);

    Task<bool> AddFriend(Guid userId, Guid friendId);

    Task<bool> AcceptFriend(Guid userId, Guid friendId);

    Task<bool> IsUsersFriends(Guid firstUser, Guid secondUser);
}
