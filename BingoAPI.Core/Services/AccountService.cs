using AutoMapper;
using BingoAPI.Core.Dtos;
using BingoAPI.Core.Entities;
using BingoAPI.Core.RepositoryContracts;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Core.ValidationHelper;

namespace BingoAPI.Core.Services;
public class AccountService : IAccountService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public AccountService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
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
        if (await _usersRepository.FindUserByEmail(registerDto.Email) is not null)
            throw new ArgumentException($"{registerDto.Email} is already registered");

        var user = await _usersRepository.SaveUser(_mapper.Map<ApplicationUser>(registerDto), registerDto.Password);

        if (user is null)
            throw new ArgumentException("Something went wrong during registration please try again later!");

        return _mapper.Map<UserResponse>(user);
    }


    public async Task SignOut() => await _usersRepository.SignOut();
}
