using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoHub.Services.Channels.Core.Entities;

namespace VideoHub.Services.Channels.Core.DAL.Configurations;

internal sealed class ChannelVideoConfiguration : IEntityTypeConfiguration<ChannelVideo>
{
    public void Configure(EntityTypeBuilder<ChannelVideo> builder)
    {
        builder.HasKey(x => new {x.ChannelId, x.VideoId});
    }
}