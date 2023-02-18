using BingoAPI.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BingoAPI.Infrastructure.DataContext;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var users = GetUserSeedData("usersSeed.json");

        builder.Entity<ApplicationUser>().HasData(users);
    }

    private List<ApplicationUser> GetUserSeedData(string fileName)
    {
        List<ApplicationUser>? usersList;

        using (StreamReader reader = new(fileName))
        {
            string json = reader.ReadToEnd();
            usersList = JsonSerializer.Deserialize<List<ApplicationUser>>(json);
        }

        var passwordHasher = new PasswordHasher<ApplicationUser>();
        if (usersList is null)
            return new List<ApplicationUser>();

        foreach(var user in usersList)
        {
            user.NormalizedUserName = user.UserName?.ToUpper();
            user.NormalizedEmail = user.Email?.ToUpper();
            user.SecurityStamp = Guid.NewGuid().ToString("D");
            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
        }

        return usersList;
    }
}
