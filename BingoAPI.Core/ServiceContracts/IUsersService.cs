﻿using BingoAPI.Core.Dtos;

namespace BingoAPI.Core.ServiceContracts;
public interface IUsersService
{
    Task<UserResponse> Register(RegisterDto registerDto);

    Task<UserResponse> Login(LoginDto loginDto);

    Task<List<UserResponse>> GetAllUsers();

    Task<List<UserResponse>> GetUsersOrderedByGameSchedule(string userName);

    Task<List<UserResponse>> GetMatchesGameSchedule(string userName, string? gameTime = null);

    Task SignOut();

    Task<UserResponse> SetGameSchedule(string username, string gameTime);
}
