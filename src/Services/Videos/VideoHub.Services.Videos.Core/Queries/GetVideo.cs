using Micro.Abstractions;
using VideoHub.Services.Videos.Core.DTO;

namespace VideoHub.Services.Videos.Core.Queries;

public record GetVideo(long VideoId) : IQuery<VideoDetailsDto?>;