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

    public async Task<List<ApplicationUser>> FindAllUsers() => await _userManager.Users.ToListAsync();

    public async Task<ApplicationUser?> FindUserByEmail(string email) => await _userManager.FindByEmailAsync(email);

    public async Task<ApplicationUser?> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        return result.Succeeded ? await FindUserByEmail(email) : null;
    }

    public async Task<ApplicationUser?> SaveUser(ApplicationUser user, string password)
    {
        user.Id = Guid.NewGuid();
        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded ? user : null;
    }

    public async Task SignOut() => await _signInManager.SignOutAsync();

    public async Task<ApplicationUser?> UpdateUser(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded ? user : null;
    }
}
