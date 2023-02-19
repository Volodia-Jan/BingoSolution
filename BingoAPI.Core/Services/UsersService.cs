using AutoMapper;
using BingoAPI.Core.Dtos;
using BingoAPI.Core.Entities;
using BingoAPI.Core.RepositoryContracts;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Core.ValidationHelper;

namespace BingoAPI.Core.Services;
public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
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
                .Where(appUser => appUser.GameSchedule == date)
                .ToList();
        matchesUsers.Remove(user);

        return _mapper.Map<List<UserResponse>>(matchesUsers);
    }

    public async Task<List<UserResponse>> GetUsersOrderedByGameSchedule(string userName)
    {
        var appUsersList = (await _usersRepository.FindAllUsers())
            .OrderBy(appUser => appUser.GameSchedule)
            .ToList();

        var authorizedUser = await _usersRepository.FindUserByEmail(userName);
        if (authorizedUser is not null)
            appUsersList.Remove(authorizedUser);

        return _mapper.Map<List<UserResponse>>(appUsersList);
    }

    public async Task<UserResponse> Login(LoginDto loginDto)
    {
        ModelValidator.Validate(loginDto);
        var user = await _usersRepository.Login(loginDto.Email, loginDto.Password);

        if (user is null)
            throw new ArgumentException("Invalid username or password");

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse> Register(RegisterDto registerDto)
    {
        ModelValidator.Validate(registerDto);
        var user = await _usersRepository.SaveUser(_mapper.Map<ApplicationUser>(registerDto), registerDto.Password);

        if (user is null)
            throw new ArgumentException("Something went wrong please try again later!");

        return _mapper.Map<UserResponse>(user);
    }

    public async Task<UserResponse> SetGameSchedule(string username, string gameTime)
    {
        if (DateTime.TryParse(gameTime, out DateTime date))
        {
            var user = await _usersRepository.UpdateUserGameSchedule(username, date);

            return _mapper.Map<UserResponse>(user);
        }

        throw new ArgumentException($"Invalid format of data:{gameTime}");
    }

    public async Task SignOut() => await _usersRepository.SignOut();
}
