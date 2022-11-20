using System.Runtime.CompilerServices;
using Micro.DAL.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoHub.Services.Users.Core.Clients;
using VideoHub.Services.Users.Core.DAL;
using VideoHub.Services.Users.Core.DAL.Repositories;
using VideoHub.Services.Users.Core.Entities;
using VideoHub.Services.Users.Core.Repositories;
using VideoHub.Services.Users.Core.Services;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace VideoHub.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddSingleton<IChannelsApiClient, ChannelsApiClient>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddPostgres<UsersDbContext>(configuration)
            .AddInitializer<UsersDataInitializer>();
}