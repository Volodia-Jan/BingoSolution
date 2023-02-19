using AutoMapper;
using BingoAPI.Core.Dtos;
using BingoAPI.Core.RepositoryContracts;
using BingoAPI.Core.ServiceContracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BingoAPI.Core.Services;
public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, IFriendshipRepository friendshipRepository)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _friendshipRepository = friendshipRepository;
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        var appUsersList = await _usersRepository.FindAllUsers();

        return _mapper.Map<List<UserResponse>>(appUsersList);
    }

    public async Task<List<UserResponse>> GetMatchesGameSchedule(string userName, string? gameTime=null)
    {
        var user = await _usersRepository.FindUserByEmail(userName);
        if (user is null)
            throw new ArgumentException($"User not found by Username:{userName}");

        DateTime date;
        if (string.IsNullOrEmpty(gameTime?.Trim()))
            date = user.GameSchedule ?? DateTime.UtcNow;
        else if (!DateTime.TryParse(gameTime, out date))
            throw new ArgumentException($"Invalid date format:{gameTime}");

        var matchesUsers = (await _usersRepository.FindAllUsers())
            .Select(async appUser =>
            {
                bool isUsersFriends = await _friendshipRepository.IsUsersFriends(appUser.Id, user.Id);
                if (appUser.Id != user.Id && 
                ((appUser.IsGameInfoPrivate && isUsersFriends) || !appUser.IsGameInfoPrivate) 
                && appUser.GameSchedule == date)
                    return appUser;

                return null;
            })
            .Select(task => task.Result)
            .Where(user => user != null && user.GameSchedule != null)
            .ToList();

        return _mapper.Map<List<UserResponse>>(matchesUsers);
    }

    public async Task<List<UserResponse>> GetUsersOrderedByGameSchedule(string userName)
    {
        var user = await _usersRepository.FindUserByEmail(userName);
        if (user is null)
            throw new ArgumentException($"User not found by Username:{userName}");

        var appUsersList = (await _usersRepository.FindAllUsers())
            .Select(async appUser =>
            {
                bool isUsersFriends = await _friendshipRepository.IsUsersFriends(appUser.Id, user.Id);
                if (appUser.Id != user.Id &&
                ((appUser.IsGameInfoPrivate && isUsersFriends) || !appUser.IsGameInfoPrivate)
                && appUser.GameSchedule != null)
                    return appUser;

                return null;
            })
            .Select(task => task.Result)
            .Where(appUser => appUser != null)
            .OrderBy(appUser => appUser.GameSchedule)
            .ToList();

        return _mapper.Map<List<UserResponse>>(appUsersList);
    }

    public async Task<UserResponse> SetGamePrivacy(string username, bool privacy)
    {
        var user = await _usersRepository.FindUserByEmail(username);

        if (user is null)
            throw new ArgumentException($"User not found by username:{username}");

        if (user.IsGameInfoPrivate != privacy)
        {
            user.IsGameInfoPrivate = privacy;
            var updatedUser = await _usersRepository.UpdateUser(user);

            return _mapper.Map<UserResponse>(updatedUser);
        }

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse> SetGameSchedule(string username, string gameTime)
    {
        if (DateTime.TryParse(gameTime, out DateTime date))
        {
            var user = await _usersRepository.FindUserByEmail(username);

            if (user is null)
                throw new ArgumentException($"User not found by username:{username}");

            user.GameSchedule = date;
            var updatedUser = await _usersRepository.UpdateUser(user);

            return _mapper.Map<UserResponse>(updatedUser);
        }

        throw new ArgumentException($"Invalid format of data:{gameTime}");
    }
}
