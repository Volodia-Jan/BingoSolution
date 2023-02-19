using AutoMapper;
using BingoAPI.Core.Dtos;
using BingoAPI.Core.RepositoryContracts;
using BingoAPI.Core.ServiceContracts;

namespace BingoAPI.Core.Services;
public class FriendshipService : IFriendshipsService
{
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public FriendshipService(IFriendshipRepository friendshipRepository, IMapper mapper, IUsersRepository usersRepository)
    {
        _friendshipRepository = friendshipRepository;
        _mapper = mapper;
        _usersRepository = usersRepository;
    }

    public async Task<bool> AcceptFriend(string username, string friendUsername)
    {
        if (username == friendUsername)
            throw new ArgumentException("You can not add yourself to friends");

        var user = await _usersRepository.FindUserByEmail(username);
        var friend = await _usersRepository.FindUserByEmail(friendUsername);
        if (user is null )
            throw new ArgumentException($"User not found by username:{username}");
        if (friend is null )
            throw new ArgumentException($"User not found by username:{friendUsername}");

        return await _friendshipRepository.AcceptFriend(user.Id, friend.Id);
    }

    public async Task<bool> AddFriend(string username, string friendUsername)
    {
        if (username == friendUsername)
            throw new ArgumentException("You can not add yourself to friends");

        var user = await _usersRepository.FindUserByEmail(username);
        var friend = await _usersRepository.FindUserByEmail(friendUsername);
        if (user is null)
            throw new ArgumentException($"User not found by username:{username}");
        if (friend is null)
            throw new ArgumentException($"User not found by username:{friendUsername}");

        return await _friendshipRepository.AddFriend(user.Id, friend.Id);
    }

    public async Task<List<UserResponse>> GetAllFriendRequests(string username)
    {
        var user = await _usersRepository.FindUserByEmail(username);
        if (user is null)
            throw new ArgumentException($"User not found by username:{username}");

        var frienships = await _friendshipRepository.FindAllFriendRequests(user.Id);

        var friends = frienships.Select(f => f.User).ToList();

        return _mapper.Map<List<UserResponse>>(friends);
    }

    public async Task<List<UserResponse>> GetAllUserFriends(string username)
    {
        var user = await _usersRepository.FindUserByEmail(username);
        if (user is null)
            throw new ArgumentException($"User not found by username:{username}");

        var frienships = await _friendshipRepository.FindAllUserFriends(user.Id);

        var friends = frienships.Select(f => f.Friend.Id == user.Id ? f.User : f.Friend).ToList();

        return _mapper.Map<List<UserResponse>>(friends);
    }
}
