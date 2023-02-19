using BingoAPI.Core.Dtos;

namespace BingoAPI.Core.ServiceContracts;

public interface IAccountService
{
    Task<UserResponse> Register(RegisterDto registerDto);

    Task<UserResponse> Login(LoginDto loginDto);

    Task SignOut();
}
