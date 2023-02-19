using BingoAPI.Core.Entities;

namespace BingoAPI.Core.RepositoryContracts;
public interface IUsersRepository
{
    Task<ApplicationUser?> SaveUser(ApplicationUser user, string password);
    Task<ApplicationUser?> Login(string email, string password);
    Task<ApplicationUser?> UpdateUser(ApplicationUser user);
    Task<List<ApplicationUser>> FindAllUsers();
    Task<ApplicationUser?> FindUserByEmail(string email);
    Task SignOut();
}
