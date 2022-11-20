using System.Runtime.CompilerServices;
using Micro.DAL.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoHub.Services.Videos.Core.Clients;
using VideoHub.Services.Videos.Core.Repositories;
using VideoHub.Services.Videos.Core.Services;
using VideoHub.Services.Videos.Infrastructure.Channels;
using VideoHub.Services.Videos.Infrastructure.Clients;
using VideoHub.Services.Videos.Infrastructure.DAL;
using VideoHub.Services.Videos.Infrastructure.DAL.Repositories;
using VideoHub.Services.Videos.Infrastructure.Renderers;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace VideoHub.Services.Videos.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var videoChannel = new VideoRendererChannel();
        return services
            .AddScoped<ISubscriptionRepository, SubscriptionRepository>()
            .AddScoped<IVideoRepository, VideoRepository>()
            .AddSingleton<IUsersApiClient, UsersApiClient>()
            .AddSingleton<IVideoRenderer>(videoChannel)
            .AddSingleton(videoChannel)
            .AddSingleton<VideoRenderer>()
            .AddHostedService<BackgroundVideoRenderer>()
            .AddPostgres<VideosDbContext>(configuration);
    }
}