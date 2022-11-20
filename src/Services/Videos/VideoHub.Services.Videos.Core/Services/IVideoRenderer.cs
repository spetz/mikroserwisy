namespace VideoHub.Services.Videos.Core.Services;

public interface IVideoRenderer
{
    Task RenderAsync(long videoId);
}