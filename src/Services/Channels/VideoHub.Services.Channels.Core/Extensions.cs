using System.Runtime.CompilerServices;
using Micro.DAL.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoHub.Services.Channels.Core.Clients;
using VideoHub.Services.Channels.Core.DAL;
using VideoHub.Services.Channels.Core.DAL.Repositories;
using VideoHub.Services.Channels.Core.Repositories;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace VideoHub.Services.Channels.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddSingleton<IVideosApiClient, VideosApiClient>()
            .AddScoped<IChannelRepository, ChannelRepository>()
            .AddScoped<IChannelSubscriptionRepository, ChannelSubscriptionRepository>()
            .AddScoped<IChannelVideoRepository, ChannelVideoRepository>()
            .AddScoped<IVideoRepository, VideoRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddPostgres<ChannelsDbContext>(configuration);
}