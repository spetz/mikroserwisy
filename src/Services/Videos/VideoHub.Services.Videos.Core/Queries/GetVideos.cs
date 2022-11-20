using Micro.Abstractions;
using VideoHub.Services.Videos.Core.DTO;

namespace VideoHub.Services.Videos.Core.Queries;

public record GetVideos(string? Title = default, long? UserId = default) : IQuery<IEnumerable<VideoDto>>;