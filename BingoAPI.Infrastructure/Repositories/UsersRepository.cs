using BingoAPI.Core.Entities;
using BingoAPI.Core.RepositoryContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BingoAPI.Infrastructure.Repositories;
public class UsersRepository : IUsersRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsersRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> DeleteUserById(Guid id)
    {
        var userToDelete = await FindUserById(id);
        if (userToDelete is null) return false;
        var result = await _userManager.DeleteAsync(userToDelete);

        return result.Succeeded;
    }

    public async Task<List<ApplicationUser>> FindAllUsers() => await _userManager.Users.ToListAsync();

    public async Task<ApplicationUser?> FindUserByEmail(string email) => await _userManager.FindByEmailAsync(email);

    public async Task<ApplicationUser?> FindUserById(Guid id) => await _userManager.FindByIdAsync(id.ToString());

    public Task<ApplicationUser?> FindUserByName(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        return result.Succeeded ? await FindUserByEmail(email) : null;
    }

    public async Task<ApplicationUser?> SaveUser(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded ? user : null;
    }

    public async Task SignOut() => await _signInManager.SignOutAsync();

    public async Task<ApplicationUser?> UpdateUserGameSchedule(string userEmail, DateTime dateTime)
    {
        var userToUpdate = await FindUserByEmail(userEmail);
        if (userToUpdate is null) return null;

        userToUpdate.GameSchedule = dateTime;
        var result = await _userManager.UpdateAsync(userToUpdate);
        
        return result.Succeeded ? userToUpdate : null;
    }
}
