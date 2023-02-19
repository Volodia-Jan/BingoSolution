using BingoAPI.Core.Entities;
using BingoAPI.Core.RepositoryContracts;
using BingoAPI.Core.ServiceContracts;
using BingoAPI.Core.Services;
using BingoAPI.Infrastructure.DataContext;
using BingoAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BingoAPI.Core.Dtos;

namespace BingoAPI.StartupExtentions;

public static class ServicesExtention
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultString"));
        });

        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IFriendshipsService, FriendshipService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IFriendshipRepository, FriendshipRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders()
           .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
           .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bingo API", Version = "v1" });
        });
    }
}
